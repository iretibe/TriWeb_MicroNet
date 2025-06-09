using FluentValidation;
using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Infrastructure.Validators
{
    public class InterestDistributionValidator : AbstractValidator<InterestDistribution>
    {
        public InterestDistributionValidator()
        {
            RuleFor(x => x.TotalInterest).GreaterThan(0);
            RuleFor(x => x.DistributedTo).NotEmpty();
        }
    }
}
