namespace MicroNet.Shared.CQRS.Events
{
    public interface IEvent
    {
        //Unique identifier for this event instance (useful for idempotency and tracing).
        Guid Id { get; }

        //Time the event occurred (usually in UTC).
        DateTime OccurredAt { get; }

        //The identifier of the user or system that triggered the event.
        string TriggeredBy { get; }

        //The name/type of the event (useful for logging and dynamic handling).
        string EventType => GetType().Name;
    }
}
