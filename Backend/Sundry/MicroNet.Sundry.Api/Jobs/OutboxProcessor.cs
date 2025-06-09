using MicroNet.Shared.CQRS.Events;
using MicroNet.Sundry.Core.Repositories.Outbox;
using System.Text.Json;

namespace MicroNet.Sundry.Api.Jobs
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _factory;
        private readonly IMessageBroker _broker;

        public OutboxProcessor(IServiceScopeFactory factory, IMessageBroker broker)
        {
            _factory = factory;
            _broker = broker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _factory.CreateScope();
                var outboxRepo = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

                var messages = await outboxRepo.GetUnprocessedMessageAsync();

                foreach (var msg in messages)
                {
                    var eventType = Type.GetType($"MicroNet.Pos.Application.Events.{msg.Type}");
                    if (eventType is null) continue;

                    var @event = (IEvent?)JsonSerializer.Deserialize(msg.Payload, eventType);
                    if (@event is null) continue;

                    await _broker.PublishAsync(@event, msg.Type);
                    await outboxRepo.MarkAsProcessedAsync(msg.Id);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
