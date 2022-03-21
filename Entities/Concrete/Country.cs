using Core.Entities;

namespace Entities.Concrete
{
    public class Country:  IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public DayOfWeek FirstWeekendDayOfWeek { get; set; }
        public DayOfWeek SecondWeekendDayOfWeek { get; set; }
    }
}
