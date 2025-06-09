using FluentValidation;
using MicroNet.Revenue.Core.Dtos;

namespace MicroNet.Revenue.Infrastructure.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.AccountName).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Reference).NotEmpty();
            RuleFor(x => x.DepositorName).NotEmpty();
            RuleFor(x => x.IdType).NotEmpty();
            RuleFor(x => x.IdNumber).NotEmpty();
            RuleFor(x => x.DestinationType).NotEmpty();
        }
    }
}
