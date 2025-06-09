using MicroNet.Device.Application.Events;
using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Handlers.Events
{
    public class DeviceAddedEventHandler : IEventHandler<DeviceAddedEvent>
    {
        public Task HandleAsync(DeviceAddedEvent @event, CancellationToken cancellationToken = default)
        {
            // Send email, publish to RabbitMQ, log, etc.
            return Task.CompletedTask;
        }
    }
}
