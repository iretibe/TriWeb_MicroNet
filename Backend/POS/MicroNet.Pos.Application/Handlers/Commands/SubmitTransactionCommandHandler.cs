using MicroNet.Pos.Application.Commands;
using MicroNet.Pos.Application.Events;
using MicroNet.Pos.Core.Entities;
using MicroNet.Pos.Core.Repositories;
using MicroNet.Pos.Core.Repositories.Outbox;
using MicroNet.Pos.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;
using System.Text.Json;

namespace MicroNet.Pos.Application.Handlers.Commands
{
    public class SubmitTransactionCommandHandler : ICommandHandler<SubmitTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _repository;
        private readonly IOutboxRepository _outboxRepository;

        public SubmitTransactionCommandHandler(ITransactionRepository repository, IOutboxRepository outboxRepository)
        {
            _repository = repository;
            _outboxRepository = outboxRepository;
        }

        public async Task<Guid> Handle(SubmitTransactionCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Transaction;

            var depositorId = new DepositorId(dto.DepositorIdType, dto.DepositorIdNumber);
            var account = new ClientAccount(dto.AccountNumber, dto.AccountName);
            //var auditInfo = new AuditInfo(dto.c)

            var transaction = new Transaction(dto.TransactionType, dto.AccountNumber,
                dto.AccountName, dto.Reference, dto.Amount, dto.PaymentChannel, dto.DepositorName,
                dto.DepositorIdType, dto.DepositorIdNumber, dto.AgentCode, dto.AgentPin, dto.DestinationNetwork,
                dto.WalletNumber, dto.OTP, dto.CreatedBy);

            await _repository.AddAsync(transaction);

            var eventToPublish = new TransactionCompletedEvent(transaction.Id, transaction.AccountName, 
                transaction.Amount, transaction.Reference, transaction.DestinationNetwork);

            var outbox = new OutboxMessage
            {
                Type = nameof(TransactionCompletedEvent),
                Payload = JsonSerializer.Serialize(eventToPublish),
                CreatedAt = DateTime.UtcNow
            };
            await _outboxRepository.AddAsync(outbox);

            return transaction.Id;
        }
    }
}
