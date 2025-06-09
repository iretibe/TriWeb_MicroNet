using MicroNet.Loan.Application.Commands;
using MicroNet.Loan.Application.Events;
using MicroNet.Loan.Core.Entities;
using MicroNet.Loan.Core.Logging;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Loan.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using System.Text.Json;

namespace MicroNet.Loan.Application.Handlers.Commands
{
    public class SubmitLoanRequestCommandHandler : ICommandHandler<SubmitLoanRequestCommand, Guid>
    {
        private readonly ILoanRequestRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public SubmitLoanRequestCommandHandler(ILoanRequestRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Guid> Handle(SubmitLoanRequestCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Request;

            var documents = dto.SupportingDocuments
                .Select(d => new LoanDocument 
                { 
                    FileName = d.FileName, 
                    FileUrl = d.FileUrl 
                })
                .ToList();

            var loan = new LoanRequest(
                dto.ClientAccountNumber,
                dto.ClientAccountName,
                dto.BranchName,
                dto.LoanType,
                dto.InterestRate,
                dto.RepaymentPeriod,
                dto.RequestedAmount,
                dto.RequestedPrincipal,
                dto.RiskMargin,
                dto.InsuranceAmount,
                dto.DisbursementMedium,
                documents,
                dto.RequestedBy
            );

            await _repository.AddAsync(loan);

            await _messageBroker.PublishAsync(new LoanRequestedEvent(loan.Id, dto.RequestedBy), "loan.requested");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(LoanRequestedEvent),
                Payload = JsonSerializer.Serialize(new LoanRequestedEvent(loan!.Id, dto.RequestedBy)),
                AggregateId = loan.Id,
                AggregateType = "LoanRequest",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return loan.Id;
        }
    }
}
