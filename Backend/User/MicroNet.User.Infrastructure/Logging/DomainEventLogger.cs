using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Logging;
using MicroNet.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Logging
{
    public class DomainEventLogger : IDomainEventLogger
    {
        private readonly UserContext _context;

        public DomainEventLogger(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DomainEventLog>> GetUnpublishedEventsAsync()
        {
            return await _context.DomainEventLogs
                .Where(e => !e.IsPublished)
                .OrderBy(e => e.LastAttemptedAt)
                .ToListAsync();
        }

        public async Task LogAsync(DomainEventLog eventLog)
        {
            _context.DomainEventLogs.Add(eventLog);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsPublishedAsync(Guid id)
        {
            var log = await _context.DomainEventLogs.FindAsync(id);
            if (log != null)
            {
                log.IsPublished = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
