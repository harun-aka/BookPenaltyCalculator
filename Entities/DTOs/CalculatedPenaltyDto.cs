using Core.Entities;

namespace Entities.DTOs
{
    public class CalculatedPenaltyDto : IDto
    {
        public int BusinessDays { get; set; }
        public decimal Penalty { get; set; }
        public string CurrencyCode { get; set; }
    }
}
