using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Employee.Application.Commands
{
    public record DeleteEmployeeCommand(Guid Id) : ICommand;
}
