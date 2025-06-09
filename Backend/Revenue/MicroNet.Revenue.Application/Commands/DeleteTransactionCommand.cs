using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record DeleteTransactionCommand(Guid Id) : ICommand;
}
