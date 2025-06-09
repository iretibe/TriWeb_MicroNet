using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Queries
{
    public record GetTransactionByIdQuery(Guid Id) : IQuery<TransactionDto>;
}
