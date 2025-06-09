using MicroNet.Device.Application.Events;
using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Device.Application.Handlers.Events
{
    public class DeviceDeletedEventHandler : IEventHandler<DeviceDeletedEvent>
    {
        public Task HandleAsync(DeviceDeletedEvent @event, CancellationToken cancellationToken = default)
        {
            // Example: Remove from cache, notify other systems, audit log, etc.
            Console.WriteLine($"Device deleted: {@event.DeviceId} by {@event.DeletedBy}");
            
            return Task.CompletedTask;
        }
    }
}
