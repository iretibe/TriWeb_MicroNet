using MicroNet.Sundry.Core.Entities;

namespace MicroNet.Sundry.Core.Repositories.Outbox
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message);
        Task<List<OutboxMessage>> GetUnprocessedMessageAsync(int maxCount = 50);
        Task MarkAsProcessedAsync(Guid id);
    }
}
