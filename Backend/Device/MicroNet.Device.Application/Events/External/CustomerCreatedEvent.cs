using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Events.External
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public Guid CustomerId { get; private set; }

        public CustomerCreatedEvent(string triggeredBy, Guid customerId)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            TriggeredBy = triggeredBy;
            CustomerId = customerId;
        }
    }
}
