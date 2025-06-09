using MicroNet.Shared.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MicroNet.Shared.CQRS.Dispatchers.Events
{
    public class InMemoryEventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InMemoryEventDispatcher> _logger;

        public InMemoryEventDispatcher(IServiceProvider serviceProvider, ILogger<InMemoryEventDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                try
                {
                    await handler.HandleAsync(@event, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling event {EventType} with handler {HandlerType}",
                        typeof(TEvent).Name, handler.GetType().Name);
                }
            }
        }
    }
}
