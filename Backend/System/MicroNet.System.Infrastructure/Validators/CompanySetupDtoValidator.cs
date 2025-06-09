using FluentValidation;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Infrastructure.Validators
{
    public class CompanySetupDtoValidator : AbstractValidator<CompanySetupDto>
    {
        public CompanySetupDtoValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CompanyAddress).NotEmpty().MaximumLength(300);
            RuleFor(x => x.RegistrationDate).LessThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.OfficialEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.OfficialPhoneNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.YearOfRegistration).NotEmpty();
            RuleFor(x => x.SSN).MaximumLength(50);
            RuleFor(x => x.TIN).MaximumLength(50);
            RuleFor(x => x.Prefix).NotEmpty().MaximumLength(20);

            RuleFor(x => x.Contact).SetValidator(new ContactPersonDtoValidator());
            RuleFor(x => x.Integration).SetValidator(new IntegrationSettingsDtoValidator());
            RuleFor(x => x.Notification).SetValidator(new NotificationSettingsDtoValidator());

            When(x => x.Logo != null, () =>
            {
                RuleFor(x => x.Logo!).SetValidator(new CompanyLogoDtoValidator());
            });
        }
    }
}
