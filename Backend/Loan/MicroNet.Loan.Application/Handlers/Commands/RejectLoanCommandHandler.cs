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
    public class RejectLoanCommandHandler : ICommandHandler<RejectLoanCommand>
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public RejectLoanCommandHandler(ILoanRequestRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Unit> Handle(RejectLoanCommand command, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(command.RequestId);
            loan?.Reject(command.RejectedBy, command.Reason);
            await _repository.UpdateAsync(loan!);

            await _messageBroker.PublishAsync(new LoanRejectedEvent(loan!.Id, command.Reason, command.RejectedBy), "loan.rejected");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(LoanRejectedEvent),
                Payload = JsonSerializer.Serialize(new LoanRejectedEvent(loan!.Id, command.Reason, command.RejectedBy)),
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
