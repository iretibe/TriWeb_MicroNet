using MediatR;

namespace MicroNet.User.Core.Entities
{
    public class AuditLog : AggregateRoot
    {
        public DateTime AuditDate { get; private set; } = DateTime.UtcNow;
        public string UserId { get; private set; }
        public string Data { get; private set; }
        public string Method { get; private set; }
        public string EntityType { get; private set; }

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        private AuditLog() { }

        public AuditLog(string userId, string data, string method, string entityType)
        {
            AuditDate = DateTime.UtcNow;
            UserId = userId;
            Data = data;
            Method = method;
            EntityType = entityType;
        }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public static AuditLog AddAuditLog(string userId, string data, string method, string entityType)
        {
            return new AuditLog(userId, data, method, entityType);
        }
    }
}
