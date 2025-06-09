using FluentValidation;
using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Infrastructure.Validators
{
    public class ManagementFeeValidator : AbstractValidator<ManagementFee>
    {
        public ManagementFeeValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.CalculatedAmount).GreaterThan(0);
        }
    }
}
