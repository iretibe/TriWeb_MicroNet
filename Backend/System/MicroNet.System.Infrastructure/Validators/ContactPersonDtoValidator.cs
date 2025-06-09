using FluentValidation;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Infrastructure.Validators
{
    public class ContactPersonDtoValidator : AbstractValidator<ContactPersonDto>
    {
        public ContactPersonDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(50);
        }
    }
}
