using MicroNet.Shared.CQRS.Events;

namespace MicroNet.User.Application.Events.Audit
{
    public class AuditLogCreatedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public string UserId { get; private set; }
        public string Data { get; private set; }
        public string Method { get; private set; }
        public string EntityType { get; private set; }

        public AuditLogCreatedEvent(string userId, string data, string method, string entityType, string triggeredBy)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.Now;
            TriggeredBy = triggeredBy;
            UserId = userId;
            Data = data;
            Method = method;
            EntityType = entityType;
        }
    }
}
