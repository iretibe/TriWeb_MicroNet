using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Commands.Audit;
using MicroNet.User.Application.Events.Audit;
using MicroNet.User.Application.Exceptions.Audit;
using MicroNet.User.Core.Dto.Audit;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Logging;
using MicroNet.User.Core.Repositories;
using System.Text.Json;

namespace MicroNet.User.Application.Handlers.Commands.Audit
{
    public class AddAuditLogCommandHandler : ICommandHandler<AddAuditLogCommand, AuditLogDto>
    {
        private readonly IAuditLogRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public AddAuditLogCommandHandler(IAuditLogRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<AuditLogDto> Handle(AddAuditLogCommand command, CancellationToken cancellationToken)
        {
            var auditLog = await _repository.GetAuditLogByIdAsync(command.Id);
            if (auditLog is not null)
            {
                throw new AuditLogAlreadyExistsException(command.Id);
            }

            var newAudit = AuditLog.AddAuditLog(command.UserId, command.Data, command.Method, command.EntityType);

            await _repository.AddAuditLogAsync(newAudit);

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new AuditLogCreatedEvent(newAudit.UserId, newAudit.Data, newAudit.Method, newAudit.EntityType, newAudit.UserId), "audit.added");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(AuditLogCreatedEvent),
                Payload = JsonSerializer.Serialize(new AuditLogCreatedEvent(newAudit.UserId, newAudit.Data, newAudit.Method, newAudit.EntityType, newAudit.UserId)),
                AggregateId = newAudit.Id,
                AggregateType = "AuditLog",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return new AuditLogDto
            {
                Id = newAudit.Id,
                AuditDate = DateTime.UtcNow,
                UserId = newAudit.UserId,
                Data = newAudit.Data,
                Method = newAudit.Method,
                EntityType = newAudit.EntityType
            };
        }
    }
}
