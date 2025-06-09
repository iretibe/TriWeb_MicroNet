using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    internal class CreateRevenueReversalCommandHandler : ICommandHandler<CreateRevenueReversalCommand, Guid>
    {
        private readonly IRevenueReversalRepository _repository;

        public CreateRevenueReversalCommandHandler(IRevenueReversalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateRevenueReversalCommand request, CancellationToken cancellationToken)
        {
            var dto = request.reversalDto;

            var reason = new ReversalReason(dto.Code, dto.Description);

            var revenueReversal = new RevenueReversal(dto.OriginalTransactionId, dto.Amount,
                reason, dto.ReversedBy, dto.CreatedBy);

            await _repository.AddAsync(revenueReversal);

            return revenueReversal.Id;
        }
    }
}
