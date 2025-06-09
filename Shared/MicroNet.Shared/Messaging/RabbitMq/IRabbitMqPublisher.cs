namespace MicroNet.Shared.Messaging.RabbitMq
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync(string exchange, string routingKey, object message, CancellationToken cancellationToken = default);
    }
}
