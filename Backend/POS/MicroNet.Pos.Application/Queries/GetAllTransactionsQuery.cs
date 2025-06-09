using MicroNet.Pos.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Queries
{
    public record GetAllTransactionsQuery : IQuery<List<TransactionDto>>;
}
