using MicroNet.Pos.Core.Entities;
using MicroNet.Pos.Core.Repositories.Outbox;
using MicroNet.Pos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Pos.Infrastructure.Repositories.Outbox
{
    public class OutboxRepository : IOutboxRepository
    { 
        private readonly PosContext _context;

        public OutboxRepository(PosContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message)
        {
            await _context.OutboxMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OutboxMessage>> GetUnprocessedMessageAsync(int maxCount = 50)
        {
            return await _context.OutboxMessages
                .Where(m => m.ProcessedAt == null)
                .OrderBy(m => m.CreatedAt)
                .Take(maxCount).ToListAsync();
        }

        public async Task MarkAsProcessedAsync(Guid id)
        {
            var message = await _context.OutboxMessages.FindAsync(id);

            if (message != null)
            {
                message.ProcessedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
