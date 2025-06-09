using MicroNet.Employee.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Employee.Application.Commands
{
    public record AddEmployeeCommand(EmployeeDto EmployeeDto, string CreatedBy) : ICommand<Guid>;
}
