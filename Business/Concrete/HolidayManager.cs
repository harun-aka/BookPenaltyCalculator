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

        public IDataResult<List<Holiday>> GetByCountryId(int countryId, DateTime timeIntervalStart, DateTime timeIntervalEnd)
        {
            return new SuccessDataResult<List<Holiday>>(_holidayDal
                .GetAll(day => day.CountryId == countryId && day.HolidayStart <= timeIntervalEnd && day.HolidayEnd >= timeIntervalStart).OrderBy(day => day.HolidayStart).ToList());
        }
    }
}
