using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Queries
{
    public record GetRevenueReversalByIdQuery(Guid Id) : IQuery<RevenueReversalDto>;
}
