using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetRevenueReversalByIdQueryHandler : IQueryHandler<GetRevenueReversalByIdQuery, RevenueReversalDto>
    {
        private readonly IRevenueReversalRepository _repository;

        public GetRevenueReversalByIdQueryHandler(IRevenueReversalRepository repository)
        {
            _repository = repository;
        }

        public async Task<RevenueReversalDto> Handle(GetRevenueReversalByIdQuery request, CancellationToken cancellationToken)
        {
            var rr = await _repository.GetByIdAsync(request.Id);
            if (rr == null) throw new RevenueReversalIdNotFoundException(request.Id);

            return new RevenueReversalDto
            {
                Id = rr.Id,
                OriginalTransactionId = rr.OriginalTransactionId,
                Amount = rr.Amount,
                Code = rr.Reason.Code,
                Description = rr.Reason.Description,
                ReversedBy = rr.ReversedBy,
                ReversedAt = rr.ReversedAt
            };
        }
    }
}
