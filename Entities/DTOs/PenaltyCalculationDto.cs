using Core.Entities;

namespace Entities.DTOs
{
    public class PenaltyCalculationDto : IDto
    {
        public DateTime CheckedOutDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int CountryId { get; set; }
    }
}
