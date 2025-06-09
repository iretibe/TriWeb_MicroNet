using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Commands.User;
using MicroNet.User.Application.Events.User;
using MicroNet.User.Application.Exceptions.User;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Logging;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.User
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeleteUserCommandHandler(IUserRepository repository, IMessageBroker messageBroker,
            IDomainEventLogger logger, IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new UserIdNotFoundException(request.Id);
            }

            await _repository.DeleteUserAsync(request.Id);
            

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new UserDeletedEvent(user.Id, user.Id, user.Id), "user.deleted");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(UserDeletedEvent),
                Payload = System.Text.Json.JsonSerializer.Serialize(new UserDeletedEvent(user.Id, user.Id, user.Id)),
                AggregateId = Guid.Parse(user.Id),
                AggregateType = "Device",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            var auditTrail = AuditLog.AddAuditLog(
                user.Id,
                Newtonsoft.Json.JsonConvert.SerializeObject(user, SerializationHelper.Settings),
                "Deleted a User",
                "AspNetUsers"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return Unit.Value;
        }
    }
}
