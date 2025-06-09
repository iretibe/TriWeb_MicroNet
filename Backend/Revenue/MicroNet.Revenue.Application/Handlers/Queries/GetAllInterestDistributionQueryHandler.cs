using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetAllInterestDistributionQueryHandler : IQueryHandler<GetAllInterestDistributionQuery, List<InterestDistributionDto>>
    {
        private readonly IInterestDistributionRepository _repository;

        public GetAllInterestDistributionQueryHandler(IInterestDistributionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InterestDistributionDto>> Handle(GetAllInterestDistributionQuery request, CancellationToken cancellationToken)
        {
            var interest = await _repository.GetAllAsync();

            return interest.Select(d => new InterestDistributionDto
            {
                Id = d.Id,
                PeriodStart = d.Period.StartDate.ToString(),
                PeriodEnd = d.Period.EndDate.ToString(),
                TotalInterest = d.TotalInterest,
                DistributedTo = d.DistributedTo.Select(a => new AccountShareDto
                {
                    AccountNumber = a.AccountNumber,
                    InterestAmount = a.ShareAmount
                }).ToList(),
                CreatedBy = d.AuditInfo.CreatedBy!,
                DistributedAt = d.DistributedAt
            }).ToList();
        }
    }
}
