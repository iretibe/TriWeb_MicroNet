using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Events
{
    public class DeviceDeletedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public Guid DeviceId { get; private set; }
        public string DeletedBy { get; private set; }

        public DeviceDeletedEvent(string triggeredBy, Guid deviceId, string deletedBy)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            TriggeredBy = triggeredBy;

            DeviceId = deviceId;
            DeletedBy = deletedBy;
        }
    }
}
