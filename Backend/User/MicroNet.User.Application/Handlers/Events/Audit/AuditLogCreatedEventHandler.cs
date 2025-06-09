using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Events.Audit;

namespace MicroNet.User.Application.Handlers.Events.Audit
{
    internal class AuditLogCreatedEventHandler : IEventHandler<AuditLogCreatedEvent>
    {
        public Task HandleAsync(AuditLogCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
