using MicroNet.Shared.CQRS.Dispatchers.Events;
using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Shared.Messaging.RabbitMq
{
    public class RabbitMqEventDispatcher : IEventDispatcher
    {
        private readonly IRabbitMqPublisher _publisher;
        private readonly RabbitMQSettings _settings;

        public RabbitMqEventDispatcher(IRabbitMqPublisher publisher, RabbitMQSettings settings)
        {
            _publisher = publisher;
            _settings = settings;
        }

        public Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            var eventName = @event.GetType().Name;

            return _publisher.PublishAsync(
                exchange: _settings.Exchange,
                routingKey: $"{_settings.RoutingKey}.{eventName}",
                message: @event,
                cancellationToken: cancellationToken
            );
        }
    }
}
