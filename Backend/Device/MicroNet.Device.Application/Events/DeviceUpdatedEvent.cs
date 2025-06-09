using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Events
{
    public class DeviceUpdatedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public Guid DeviceId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public DeviceUpdatedEvent(string triggeredBy, Guid deviceId, 
            string code, string name, string updatedBy, DateTime updatedAt)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            TriggeredBy = triggeredBy;
            DeviceId = deviceId;
            Code = code;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
