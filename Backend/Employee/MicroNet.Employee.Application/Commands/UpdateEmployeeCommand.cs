using MicroNet.Employee.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Employee.Application.Commands
{
    public record UpdateEmployeeCommand(Guid Id, EmployeeDto EmployeeDto, string UpdatedBy) : ICommand;
}
