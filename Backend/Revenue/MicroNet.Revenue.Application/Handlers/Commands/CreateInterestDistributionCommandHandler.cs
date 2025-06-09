using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    internal class CreateInterestDistributionCommandHandler : ICommandHandler<CreateInterestDistributionCommand, Guid>
    {
        private readonly IInterestDistributionRepository _repository;

        public CreateInterestDistributionCommandHandler(IInterestDistributionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateInterestDistributionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.interestDto;

            // Convert DTO to domain entities
            var distributionPeriod = new DistributionPeriod(dto.PeriodStart, dto.PeriodEnd);
            var distributedTo = dto.DistributedTo.Select(s =>
                new AccountShare(s.AccountNumber, s.InterestAmount)).ToList();

            var interestDistribution = new InterestDistribution(
                distributionPeriod,
                dto.TotalInterest,
                distributedTo,
                dto.CreatedBy
            );

            await _repository.AddAsync(interestDistribution);

            return interestDistribution.Id;
        }
    }
}
