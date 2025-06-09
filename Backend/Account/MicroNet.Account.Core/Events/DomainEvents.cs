namespace MicroNet.Account.Core.Events
{
    public static class DomainEvents
    {
        private static readonly List<Action<IDomainEvent>> _handlers = new();

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            _handlers.Add(e => callback((T)e));
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            foreach (var handler in _handlers)
            {
                handler(domainEvent);
            }
        }
    }
}
