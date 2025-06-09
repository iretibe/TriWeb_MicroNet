using FluentValidation;
using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Infrastructure.Validators
{
    public class RevenueReversalValidator : AbstractValidator<RevenueReversal>
    {
        public RevenueReversalValidator()
        {
            RuleFor(x => x.OriginalTransactionId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.ReversedBy).NotEmpty().MaximumLength(100);
        }
    }
}
