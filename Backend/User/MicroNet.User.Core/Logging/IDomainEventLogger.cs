using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Logging
{
    public interface IDomainEventLogger
    {
        Task LogAsync(DomainEventLog domainEventLog);
        Task<IEnumerable<DomainEventLog>> GetUnpublishedEventsAsync();
        Task MarkAsPublishedAsync(Guid id);
    }
}
