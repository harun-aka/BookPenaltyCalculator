using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCountryDal : EfEntityRepositoryBase<Country, LibraryContext>, ICountryDal
    {
        public CountryCurrencyDto GetCountryCurrenyByCountryId(int countryId)
        {
            using (LibraryContext context = new LibraryContext())
            {
                var result = (from co  in context.Countries
                              join cur in context.Currencies on co.CurrencyId equals cur.Id
                              where co.Id == countryId
                              select new CountryCurrencyDto
                              {
                                  CountryId = co.Id,
                                  CountryName = co.Name,
                                  FirstWeekendDayOfWeek = co.FirstWeekendDayOfWeek,
                                  SecondWeekendDayOfWeek = co.SecondWeekendDayOfWeek,
                                  CurrencyId = cur.Id,
                                  CurrencyCode = cur.Code,
                                  CurrencyValue = cur.Value,
                              });
                return result.SingleOrDefault();
            }
        }
    }
}
