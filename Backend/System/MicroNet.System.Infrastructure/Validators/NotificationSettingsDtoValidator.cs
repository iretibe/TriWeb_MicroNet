using FluentValidation;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Infrastructure.Validators
{
    public class NotificationSettingsDtoValidator : AbstractValidator<NotificationSettingsDto>
    {
        public NotificationSettingsDtoValidator()
        {
            RuleFor(x => x.Mode).NotEmpty(); // Can add enum check
            RuleForEach(x => x.Recipients).NotEmpty().EmailAddress();
        }
    }
}
