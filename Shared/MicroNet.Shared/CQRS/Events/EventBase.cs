namespace MicroNet.Shared.CQRS.Events
{
    public class EventBase : IEvent
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public DateTime OccurredAt { get; protected set; } = DateTime.UtcNow;

        public string TriggeredBy { get; protected set; } = default!;

        //public string CorrelationId { get; protected set; } = default!;

        //public string? CausationId { get; protected set; }

        //public int Version { get; protected set; } = 1;
    }
}
