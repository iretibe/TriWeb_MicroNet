using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record UpdateTransactionCommand(Guid Id, TransactionDto TransDto, string UpdatedBy) : ICommand;
}
