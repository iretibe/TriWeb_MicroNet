using MicroNet.Pos.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Queries
{
    public record GetTransactionByIdQuery(Guid Id) : IQuery<TransactionDto>;
}
