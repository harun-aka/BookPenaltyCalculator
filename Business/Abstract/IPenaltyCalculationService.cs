using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IPenaltyCalculationService
    {
        IDataResult<CalculatedPenaltyDto> CalculatePenalty(PenaltyCalculationDto penaltyCalculationDto);
    }
}
