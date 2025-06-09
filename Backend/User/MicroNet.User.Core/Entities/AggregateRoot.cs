using MicroNet.User.Core.Events;

namespace MicroNet.User.Core.Entities
{
    public abstract class AggregateRoot
    {
        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> Events => _domainEvents.AsReadOnly();

        protected void AddEvent(IDomainEvent @event)
        {
            // Implementation for adding domain events
            if (!_domainEvents.Any())
            {
                Version++;
            }

            _domainEvents.Add(@event);
        }

        public void ClearEvents() => _domainEvents.Clear();
    }
}
