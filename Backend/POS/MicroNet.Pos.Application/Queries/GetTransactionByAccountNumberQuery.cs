using MicroNet.Pos.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Queries
{
    public record GetTransactionByAccountNumberQuery(string AccountNumber) : IQuery<TransactionDto>;
}
