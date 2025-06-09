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
    public class CancelLoanCommandHandler : ICommandHandler<CancelLoanCommand>
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public CancelLoanCommandHandler(ILoanRequestRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelLoanCommand command, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(command.RequestId);
            loan?.Cancel(command.CancelledBy, command.Reason);
            await _repository.UpdateAsync(loan!);

            await _messageBroker.PublishAsync(new LoanCancelledEvent(loan!.Id, command.Reason, command.CancelledBy), "loan.cancelled");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(LoanCancelledEvent),
                Payload = JsonSerializer.Serialize(new LoanCancelledEvent(loan!.Id, command.Reason, command.CancelledBy)),
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
