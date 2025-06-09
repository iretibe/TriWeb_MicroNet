using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetInterestDistributionByIdQueryHandler : IQueryHandler<GetInterestDistributionByIdQuery, InterestDistributionDto>
    {
        private readonly IInterestDistributionRepository _repository;

        public GetInterestDistributionByIdQueryHandler(IInterestDistributionRepository repository)
        {
            _repository = repository;
        }

        public async Task<InterestDistributionDto> Handle(GetInterestDistributionByIdQuery request, CancellationToken cancellationToken)
        {
            var d = await _repository.GetByIdAsync(request.Id);
            if (d == null) throw new InterestDistributionIdNotFoundException(request.Id);

            return new InterestDistributionDto
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
            };
        }
    }
}
