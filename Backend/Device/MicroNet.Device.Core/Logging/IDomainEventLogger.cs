using MicroNet.Device.Core.Entities;

namespace MicroNet.Device.Core.Logging
{
    public interface IDomainEventLogger
    {
        Task LogAsync(DomainEventLog domainEventLog);
        Task<IEnumerable<DomainEventLog>> GetUnpublishedEventsAsync();
        Task MarkAsPublishedAsync(Guid id);
    }
}
