using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICountryDal : IEntityRepository<Country>
    {
        CountryCurrencyDto GetCountryCurrenyByCountryId(int countryId);
    }
}
