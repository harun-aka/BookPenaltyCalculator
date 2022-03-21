using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class HolidayManager : IHolidayService
    {
        IHolidayDal _holidayDal;

        public HolidayManager(IHolidayDal holidayDal)
        {
            _holidayDal = holidayDal;
        }

        public IDataResult<List<Holiday>> GetByCountryIdAndDates(int countryId, DateTime timeIntervalStart, DateTime timeIntervalEnd)
        {
            return new SuccessDataResult<List<Holiday>>(_holidayDal
                .GetAll(day => day.CountryId == countryId && day.StartDate <= timeIntervalEnd && day.EndDate >= timeIntervalStart).OrderBy(day => day.StartDate).ToList());
        }
    }
}
