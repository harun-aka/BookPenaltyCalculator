using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        ICurrencyService _currencyService;
        IHolidayService _nonBusinessDayService;

        public PenaltyCalculationManager(ICountryService countryService, ICurrencyService currencyService, IHolidayService nonBusinessDayService)
        {
            _countryService = countryService;
            _currencyService = currencyService;
            _nonBusinessDayService = nonBusinessDayService;
        }

        [ValidationAspect(typeof(PenaltyCalculationDtoValidator))]
        public IDataResult<CalculatedPenaltyDto> CalculatePenalty(PenaltyCalculationDto penaltyCalculationDto)
        {
            IDataResult<List<Holiday>> resultholidayList = _nonBusinessDayService.GetByCountryId(penaltyCalculationDto.CountryId, penaltyCalculationDto.CheckedOutDate, penaltyCalculationDto.ReturnedDate);
            if(resultholidayList == null)
            {
                return new ErrorDataResult<CalculatedPenaltyDto>(Messages.NonBusinesDaysNotFound);
            }

            List<Holiday> nonBusinessDayList = resultholidayList.Data;

            IDataResult<CountryCurrencyDto> resultCountryCurrencyDto = _countryService.GetCountryCurrenyByCountryId(penaltyCalculationDto.CountryId);
            if(!resultCountryCurrencyDto.Success)
            {
                return new ErrorDataResult<CalculatedPenaltyDto>(resultCountryCurrencyDto.Message);
            }

            CountryCurrencyDto countryCurrencyDto = resultCountryCurrencyDto.Data;

            int businessDays = 0;
            for (DateTime date = penaltyCalculationDto.CheckedOutDate; date < penaltyCalculationDto.CheckedOutDate; date.AddDays(1))
            {
                IResult result = BusinessRules.Run(CheckIfDateNotInWeekend(countryCurrencyDto, date), CheckIfDateNotInHolidayList(nonBusinessDayList, date));
                if(!result.Success)
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
            int dailyPenaltyAmount = 5;
            int lateDays = businessDays - 10;
            if (lateDays <= 0)
            {
                return decimal.Zero;
            }

            return currencyValue * lateDays * dailyPenaltyAmount;
        }

        private static IResult CheckIfDateNotInHolidayList(List<Holiday> holidayList, DateTime date)
        {
            foreach (var day in holidayList)
            {
                if (day.HolidayStart <= date && day.HolidayEnd >= date)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}
