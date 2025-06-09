using FluentValidation;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Infrastructure.Validators
{
    public class IntegrationSettingsDtoValidator : AbstractValidator<IntegrationSettingsDto>
    {
        public IntegrationSettingsDtoValidator()
        {
            RuleFor(x => x.SftpPath).MaximumLength(200);
            RuleFor(x => x.ExportFolderPath).MaximumLength(200);
        }
    }
}
