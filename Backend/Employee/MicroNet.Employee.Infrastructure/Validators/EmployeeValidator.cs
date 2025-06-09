using FluentValidation;
using MicroNet.Employee.Core.Dtos;

namespace MicroNet.Employee.Infrastructure.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.EmployeeNumber).NotEmpty();
            RuleFor(e => e.FirstName).NotEmpty();
            RuleFor(e => e.Surname).NotEmpty();
            RuleFor(e => e.EmailAddress).EmailAddress();
            RuleFor(e => e.PhoneNumber).NotEmpty();
            RuleFor(e => e.DateOfBirth).LessThan(DateTime.Now);
        }
    }
}
