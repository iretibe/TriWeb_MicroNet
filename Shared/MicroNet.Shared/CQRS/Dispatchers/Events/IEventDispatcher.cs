using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Shared.CQRS.Dispatchers.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent;
    }
}
