namespace MicroNet.Device.Core.Entities
{
    public class DomainEventLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EventType { get; set; } = default!;
        public string Payload { get; set; } = default!;
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; } = "Device";
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public string? ErrorMessage { get; set; }
        public int Retries { get; set; } = 0;
        public DateTime LastAttemptedAt { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}
