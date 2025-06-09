using MicroNet.Shared.CQRS.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;

namespace MicroNet.Shared.Messaging.RabbitMq
{
    public class RabbitMQMessageBroker : IMessageBroker
    {
        private readonly RabbitMQSettings _settings;


        public RabbitMQMessageBroker(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, string routingKey) where TEvent : class, IEvent
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

            //Declare the exchange if it doesn't exist
            await channel.ExchangeDeclareAsync(exchange: _settings.Exchange, type: ExchangeType.Topic, durable: true);

            //Declare the queue if it doesn't exist
            await channel.QueueDeclareAsync(queue: _settings.Queue, durable: true, exclusive: false, autoDelete: false);

            //Bind the queue to the exchange with the routing key
            await channel.QueueBindAsync(queue: _settings.Queue, exchange: _settings.Exchange, routingKey: routingKey);

            //Set the message properties
            var json = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(json);

            //Publish the message to the exchange with the routing key
            await channel.BasicPublishAsync(exchange: _settings.Exchange, routingKey: _settings.Queue, body: body);
        }

        //public async void Subscribe<TEvent>(string queue, Func<TEvent, Task> handler) where TEvent : class, IEvent
        //{
        //    var factory = new ConnectionFactory
        //    {
        //        HostName = _settings.Host,
        //        Port = _settings.Port,
        //        UserName = _settings.Username,
        //        Password = _settings.Password,
        //        VirtualHost = _settings.VirtualHost
        //    };

        //    using var connection = await factory.CreateConnectionAsync();
        //    using var channel = await connection.CreateChannelAsync();

        //    //Declare the queue if it doesn't exist
        //    await channel.QueueDeclareAsync(queue: _settings.Queue, durable: true, exclusive: false, autoDelete: false);

        //    //Bind the queue to the exchange with the routing key
        //    await channel.QueueBindAsync(queue: _settings.Queue, exchange: _settings.Exchange, routingKey: queue);

        //    var consumer = new AsyncEventingBasicConsumer(channel);
        //    consumer.ReceivedAsync += async (model, ea) =>
        //    {
        //        var body = ea.Body.ToArray();
        //        var json = Encoding.UTF8.GetString(body);

        //        var message = JsonSerializer.Deserialize<TEvent>(json);

        //        if (message != null)
        //        {
        //            await handler(message);
        //        }

        //        await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
        //    };

        //    await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);
        //}
    }
}
