using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Revenue.Infrastructure.Repositories
{
    public class RevenueReversalRepository : IRevenueReversalRepository
    {
        private readonly RevenueContext _context;

        public RevenueReversalRepository(RevenueContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RevenueReversal reversal)
        {
            _context.RevenueReversals.Add(reversal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = await _context.RevenueReversals.FindAsync(id);
            if (query != null)
            {
                _context.RevenueReversals.Remove(query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RevenueReversal>> GetAllAsync()
        {
            return await _context.RevenueReversals.ToListAsync();
        }

        public async Task<RevenueReversal> GetByIdAsync(Guid id)
        {
            var query = await _context.RevenueReversals.FindAsync(id);
            return query!;
        }

        public async Task UpdateAsync(RevenueReversal reversal)
        {
            _context.RevenueReversals.Update(reversal);
            await _context.SaveChangesAsync();
        }
    }
}
