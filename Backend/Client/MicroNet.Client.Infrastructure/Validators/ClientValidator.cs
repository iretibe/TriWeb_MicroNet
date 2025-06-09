using FluentValidation;

namespace MicroNet.Client.Infrastructure.Validators
{
    public class ClientValidator : AbstractValidator<Core.Entities.Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow);
        }
    }
}
