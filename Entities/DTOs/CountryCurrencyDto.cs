using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CountryCurrencyDto : IDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CurrencyId { get; set; }
        public DayOfWeek FirstWeekendDayOfWeek { get; set; }
        public DayOfWeek SecondWeekendDayOfWeek { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyValue { get; set; }
    }
}
