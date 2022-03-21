using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CountryManager : ICountryService
    {
        ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public IDataResult<List<Country>> GetAll()
        {
            return new SuccessDataResult<List<Country>>(_countryDal.GetAll());
        }

        public IDataResult<CountryCurrencyDto> GetCountryCurrenyByCountryId(int countryId)
        {
            CountryCurrencyDto countryCurrencyDto = _countryDal.GetCountryCurrenyByCountryId(countryId);
            if(countryCurrencyDto == null)
            {
                return new ErrorDataResult<CountryCurrencyDto>(Messages.CountryNotFound);
            }
            return new SuccessDataResult<CountryCurrencyDto>(countryCurrencyDto);
        }
    }
}
