using MicroNet.Pos.Core.Entities;

namespace MicroNet.Pos.Core.Repositories.Outbox
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message);
        Task<List<OutboxMessage>> GetUnprocessedMessageAsync(int maxCount = 50);
        Task MarkAsProcessedAsync(Guid id);
    }
}
