using FluentValidation;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Infrastructure.Validators
{
    public class CompanyLogoDtoValidator : AbstractValidator<CompanyLogoDto>
    {
        public CompanyLogoDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
