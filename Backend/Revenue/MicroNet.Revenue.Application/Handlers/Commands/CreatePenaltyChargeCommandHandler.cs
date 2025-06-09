using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    internal class CreatePenaltyChargeCommandHandler : ICommandHandler<CreatePenaltyChargeCommand, Guid>
    {
        private readonly IPenaltyChargeRepository _repository;

        public CreatePenaltyChargeCommandHandler(IPenaltyChargeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreatePenaltyChargeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.chargedto;

            var reason = new PenaltyReason(dto.Code, dto.Description);

            var penaltyCharges = new PenaltyCharge(dto.AccountNumber, dto.Amount, reason, dto.CreatedBy);

            await _repository.AddAsync(penaltyCharges);

            return penaltyCharges.Id;
        }
    }
}
