using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace MicroNet.Shared.Messaging.RabbitMq
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly RabbitMQSettings _settings;
        private readonly ILogger<RabbitMqPublisher> _logger;

        public RabbitMqPublisher(IOptions<RabbitMQSettings> options, ILogger<RabbitMqPublisher> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task PublishAsync(string exchange, string routingKey, object message, CancellationToken cancellationToken = default)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.Username,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            //Declare the exchange if it doesn't exist
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic, durable: true);

            //Declare the queue if it doesn't exist
            await channel.QueueDeclareAsync(queue: _settings.Queue, durable: true, exclusive: false, autoDelete: false);

            //Bind the queue to the exchange with the routing key
            await channel.QueueBindAsync(queue: _settings.Queue, exchange: exchange, routingKey: routingKey);

            _logger.LogInformation("Published message to exchange '{Exchange}' with routing key '{RoutingKey}'", exchange, routingKey);

            //Publish the message to the exchange with the routing key
            // The message will be sent to the queue bound to the exchange with the routing key
            await channel.BasicPublishAsync(exchange: exchange, routingKey: _settings.Queue, body: body);
        }
    }
}
