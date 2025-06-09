using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Logging;
using MicroNet.Device.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Device.Infrastructure.Logging
{
    public class DomainEventLogger : IDomainEventLogger
    {
        private readonly DeviceContext _context;

        public DomainEventLogger(DeviceContext context)
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
