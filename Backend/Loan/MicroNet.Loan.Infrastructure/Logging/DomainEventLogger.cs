using MicroNet.Loan.Core.Entities;
using MicroNet.Loan.Core.Logging;
using MicroNet.Loan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Loan.Infrastructure.Logging
{
    public class DomainEventLogger : IDomainEventLogger
    {
        private readonly LoanContext _context;

        public DomainEventLogger(LoanContext context)
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
