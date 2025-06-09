using MicroNet.Sundry.Core.Entities;
using MicroNet.Sundry.Core.Repositories.Outbox;
using MicroNet.Sundry.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Sundry.Infrastructure.Repositories.Outbox
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly AccountingContext _context;

        public OutboxRepository(AccountingContext context)
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
