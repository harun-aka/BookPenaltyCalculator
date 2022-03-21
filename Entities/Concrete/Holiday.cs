using Core.Entities;

namespace Entities.Concrete
{
    public class Holiday: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HolidayStart { get; set; }
        public DateTime HolidayEnd { get; set; }
        public int CountryId { get; set; }
    }
}
