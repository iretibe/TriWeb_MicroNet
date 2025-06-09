using MediatR;
using MicroNet.Loan.Application.Commands;
using MicroNet.Loan.Application.Events;
using MicroNet.Loan.Core.Entities;
using MicroNet.Loan.Core.Logging;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using System.Text.Json;

namespace MicroNet.Loan.Application.Handlers.Commands
{
    public class SuspendLoanCommandHandler : ICommandHandler<SuspendLoanCommand>
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public SuspendLoanCommandHandler(ILoanRequestRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Unit> Handle(SuspendLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(request.RequestId);
            loan?.Suspend(request.SuspendedBy, request.Reason);
            await _repository.UpdateAsync(loan!);

            await _messageBroker.PublishAsync(new LoanSuspendedEvent(loan!.Id, request.Reason, request.DurationDays, request.SuspendedBy), "loan.suspended");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(LoanSuspendedEvent),
                Payload = JsonSerializer.Serialize(new LoanSuspendedEvent(loan!.Id, request.Reason, request.DurationDays, request.SuspendedBy)),
                AggregateId = loan.Id,
                AggregateType = "LoanRequest",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return Unit.Value;
        }
    }
}
