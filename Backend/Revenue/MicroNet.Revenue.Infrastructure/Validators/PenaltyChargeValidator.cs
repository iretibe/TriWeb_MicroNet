using FluentValidation;
using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Infrastructure.Validators
{
    public class PenaltyChargeValidator : AbstractValidator<PenaltyCharge>
    {
        public PenaltyChargeValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
