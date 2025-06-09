using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    internal class GetAllPenaltyChargeQueryHandler : IQueryHandler<GetAllPenaltyChargeQuery, List<PenaltyChargeDto>>
    {
        private readonly IPenaltyChargeRepository _repository;

        public GetAllPenaltyChargeQueryHandler(IPenaltyChargeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PenaltyChargeDto>> Handle(GetAllPenaltyChargeQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllAsync();

            return loans.Select(pc => new PenaltyChargeDto
            {
                Id = pc.Id,
                AccountNumber = pc.AccountNumber,
                Amount = pc.Amount,
                Code = pc.Reason.Code,
                Description = pc.Reason.Description,
                CreatedBy = pc.AuditInfo.CreatedBy!,
                ChargedAt = pc.ChargedAt
            }).ToList();
        }
    }
}
