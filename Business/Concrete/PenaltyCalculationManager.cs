using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class PenaltyCalculationManager : IPenaltyCalculationService
    {
        ICountryService _countryService;
        IHolidayService _holidayService;

        public PenaltyCalculationManager(ICountryService countryService, IHolidayService holidayService)
        {
            _countryService = countryService;
            _holidayService = holidayService;
        }

        [ValidationAspect(typeof(PenaltyCalculationDtoValidator))]
        [TransactionScopeAspect()]
        public IDataResult<CalculatedPenaltyDto> CalculatePenalty(PenaltyCalculationDto penaltyCalculationDto)
        {
            //Get Holiday List Country Id
            IDataResult<List<Holiday>> resultholidayList = _holidayService.GetByCountryIdAndDates(penaltyCalculationDto.CountryId, penaltyCalculationDto.CheckedOutDate, penaltyCalculationDto.ReturnedDate);
            if(resultholidayList == null)
            {
                return new ErrorDataResult<CalculatedPenaltyDto>(Messages.HolidayNotFound);
            }
            List<Holiday> holidayList = resultholidayList.Data;


            IDataResult<CountryCurrencyDto> resultCountryCurrencyDto = _countryService.GetCountryCurrenyByCountryId(penaltyCalculationDto.CountryId);
            if(!resultCountryCurrencyDto.Success)
            {
                return new ErrorDataResult<CalculatedPenaltyDto>(resultCountryCurrencyDto.Message);
            }

            CountryCurrencyDto countryCurrencyDto = resultCountryCurrencyDto.Data;

            //If date is not in weekend or not in holiday list, the date is a business day. 
            int businessDays = 0;
            for (DateTime date = penaltyCalculationDto.CheckedOutDate; date < penaltyCalculationDto.ReturnedDate; date = date.AddDays(1))
            {
                IResult result = BusinessRules.Run(CheckIfDateNotInWeekend(countryCurrencyDto, date), CheckIfDateNotInHolidayList(holidayList, date));
                if(result != null)
                {
                    continue;
                }

                businessDays++;
            }

            decimal penaltyAmount = CalculatePenaltyAmount(businessDays, countryCurrencyDto.CurrencyValue);

            return new SuccessDataResult<CalculatedPenaltyDto>(new CalculatedPenaltyDto
            {
                BusinessDays = businessDays,
                Penalty = penaltyAmount,
                CurrencyCode = countryCurrencyDto.CurrencyCode
            });
        }

        //Checks The Date, FirstWeekendDayOfWeek and SecondWeekendDayOfWeek weekend days of the country
        private static IResult CheckIfDateNotInWeekend(CountryCurrencyDto countryCurrencyDto, DateTime date)
        {
            if(date.DayOfWeek == countryCurrencyDto.FirstWeekendDayOfWeek || date.DayOfWeek == countryCurrencyDto.SecondWeekendDayOfWeek)
            {
                return new ErrorResult();
            }
            return new SuccessResult(); 
        }

        private static decimal CalculatePenaltyAmount(int businessDays, decimal currencyValue)
        {
            // Magic Numbers 
            int dailyPenaltyAmount = 5;
            int lateDays = businessDays - 10;
            if (lateDays <= 0)
            {
                return decimal.Zero;
            }

            //Currency is dollar indexed. The other currency values added according to dollar.
            return currencyValue * lateDays * dailyPenaltyAmount;
        }

        //Checks The Date, Holiday of the country
        private static IResult CheckIfDateNotInHolidayList(List<Holiday> holidayList, DateTime date)
        {
            foreach (var day in holidayList)
            {
                if (day.StartDate <= date && day.EndDate >= date)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}
