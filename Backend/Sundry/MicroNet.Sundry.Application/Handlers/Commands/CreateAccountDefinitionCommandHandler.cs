using MicroNet.Shared.CQRS.Commands;
using MicroNet.Sundry.Application.Commands;
using MicroNet.Sundry.Application.Events;
using MicroNet.Sundry.Core.Entities;
using MicroNet.Sundry.Core.Enums;
using MicroNet.Sundry.Core.Repositories;
using MicroNet.Sundry.Core.Repositories.Outbox;
using System.Text.Json;

namespace MicroNet.Sundry.Application.Handlers.Commands
{
    public class CreateAccountDefinitionCommandHandler : ICommandHandler<CreateAccountDefinitionCommand, Guid>
    {
        private readonly IAccountingRepository _repository;
        private readonly IOutboxRepository _outboxRepository;

        public CreateAccountDefinitionCommandHandler(IAccountingRepository repository, IOutboxRepository outboxRepository)
        {
            _repository = repository;
            _outboxRepository = outboxRepository;
        }

        public async Task<Guid> Handle(CreateAccountDefinitionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Account;
            var category = new AccountCategory();

            var account = new Accounting(
                dto.Code,
                dto.Name,
                category,
                dto.CreatedBy
            );

            await _repository.AddAsync(account);

            var eventToPublish = new AccountingCreatedEvent(account.Id, account.Code,
                account.Name, account.AuditInfo.CreatedBy!);

            var outbox = new OutboxMessage
            {
                Type = nameof(AccountingCreatedEvent),
                Payload = JsonSerializer.Serialize(eventToPublish),
                CreatedAt = DateTime.UtcNow
            };
            await _outboxRepository.AddAsync(outbox);

            return account.Id;
        }
    }
}
