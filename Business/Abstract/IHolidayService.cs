using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IHolidayService
    {
        IDataResult<List<Holiday>> GetByCountryId(int countryId, DateTime timeIntervalStart, DateTime timeIntervalEnd);
    }
}
