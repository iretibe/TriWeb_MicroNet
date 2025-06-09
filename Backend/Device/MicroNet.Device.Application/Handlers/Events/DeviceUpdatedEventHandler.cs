using MicroNet.Device.Application.Events;
using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Handlers.Events
{
    public class DeviceUpdatedEventHandler : IEventHandler<DeviceUpdatedEvent>
    {
        public Task HandleAsync(DeviceUpdatedEvent @event, CancellationToken cancellationToken = default)
        {
            // Example: Log update, publish to message broker, trigger sync, etc.
            Console.WriteLine($"Device updated: {@event.DeviceId} by {@event.UpdatedBy}");
            
            return Task.CompletedTask;
        }
    }
}
