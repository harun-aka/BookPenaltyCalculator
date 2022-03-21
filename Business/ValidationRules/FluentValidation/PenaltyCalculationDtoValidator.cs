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
            RuleFor(q => q.CheckedOutDate).NotNull();
            RuleFor(q => q.ReturnedDate).NotNull();
            RuleFor(q => q.CountryId).NotNull();
        }
    }
}
