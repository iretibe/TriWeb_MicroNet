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
    public class ApproveLoanCommandHandler : ICommandHandler<ApproveLoanCommand>
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public ApproveLoanCommandHandler(ILoanRequestRepository repository, 
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Unit> Handle(ApproveLoanCommand command, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(command.RequestId);
            loan?.Approve(command.ApprovedBy);
            await _repository.UpdateAsync(loan!);

            await _messageBroker.PublishAsync(new LoanApprovedEvent(loan!.Id, command.ApprovedBy, command.ApprovedBy), "loan.approved");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(LoanApprovedEvent),
                Payload = JsonSerializer.Serialize(new LoanApprovedEvent(loan!.Id, command.ApprovedBy, command.ApprovedBy)),
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
