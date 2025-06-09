namespace MicroNet.Shared.CQRS.Events
{
    public interface IMessageBroker
    {
        Task PublishAsync<TEvent>(TEvent @event, string routingKey)
            where TEvent : class, IEvent;

        //void Subscribe<TEvent>(string queue, Func<TEvent, Task> handler)
        //    where TEvent : class, IEvent;
    }
}
