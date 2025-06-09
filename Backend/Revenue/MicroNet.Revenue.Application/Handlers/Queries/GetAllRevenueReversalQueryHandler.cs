using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    internal class GetAllRevenueReversalQueryHandler : IQueryHandler<GetAllRevenueReversalQuery, List<RevenueReversalDto>>
    {
        private readonly IRevenueReversalRepository _repository;

        public GetAllRevenueReversalQueryHandler(IRevenueReversalRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RevenueReversalDto>> Handle(GetAllRevenueReversalQuery request, CancellationToken cancellationToken)
        {
            var revenues = await _repository.GetAllAsync();

            return revenues.Select(rr => new RevenueReversalDto
            {
                Id = rr.Id,
                OriginalTransactionId = rr.OriginalTransactionId,
                Amount = rr.Amount,
                Code = rr.Reason.Code,
                Description = rr.Reason.Description,
                ReversedBy = rr.ReversedBy,
                ReversedAt = rr.ReversedAt
            }).ToList();
        }
    }
}
