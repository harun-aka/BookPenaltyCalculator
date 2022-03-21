using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICountryService
    {
        IDataResult<CountryCurrencyDto> GetCountryCurrenyByCountryId(int countryId);
        IDataResult<List<Country>> GetAll();
    }
}
