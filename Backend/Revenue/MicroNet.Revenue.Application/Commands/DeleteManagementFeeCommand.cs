using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record DeleteManagementFeeCommand(Guid Id) : ICommand;
}
