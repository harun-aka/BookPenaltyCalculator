using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PenaltyCalculationDtoValidator : AbstractValidator<PenaltyCalculationDto>
    {
        public PenaltyCalculationDtoValidator()
        {
            RuleFor(pc => pc.CheckedOutDate).NotNull();
            RuleFor(pc => pc.ReturnedDate).NotNull();
            RuleFor(pc => pc.CountryId).NotNull();
        }

    }
}
