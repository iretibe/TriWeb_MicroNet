using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Events
{
    public class DeviceAddedEvent : IEvent
    {
        public Guid Id { get; private set; }

        public DateTime OccurredAt { get; private set; }

        public string TriggeredBy { get; private set; }

        public Guid DeviceId { get; private set; }
        public string Code { get; private set; }

        public DeviceAddedEvent(Guid deviceId, string code, string triggeredBy)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            TriggeredBy = triggeredBy;

            DeviceId = deviceId;
            Code = code;
        }
    }
}
