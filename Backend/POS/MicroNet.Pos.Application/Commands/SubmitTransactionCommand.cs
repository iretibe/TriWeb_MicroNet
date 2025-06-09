using MicroNet.Pos.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Pos.Application.Commands
{
    public record SubmitTransactionCommand(TransactionDto Transaction) : ICommand<Guid>;
}
