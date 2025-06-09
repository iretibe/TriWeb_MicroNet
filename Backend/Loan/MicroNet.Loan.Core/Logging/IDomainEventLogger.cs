using MicroNet.Loan.Core.Entities;

namespace MicroNet.Loan.Core.Logging
{
    public interface IDomainEventLogger
    {
        Task LogAsync(DomainEventLog domainEventLog);
        Task<IEnumerable<DomainEventLog>> GetUnpublishedEventsAsync();
        Task MarkAsPublishedAsync(Guid id);
    }
}
