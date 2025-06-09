using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Revenue.Infrastructure.Repositories
{
    public class InterestDistributionRepository : IInterestDistributionRepository
    {
        private readonly RevenueContext _context;

        public InterestDistributionRepository(RevenueContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InterestDistribution distribution)
        {
            _context.InterestDistributions.Add(distribution);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = await _context.InterestDistributions.FindAsync(id);
            if (query != null)
            {
                _context.InterestDistributions.Remove(query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InterestDistribution>> GetAllAsync()
        {
            return await _context.InterestDistributions.ToListAsync();
        }

        public async Task<InterestDistribution> GetByIdAsync(Guid id)
        {
            var query = await _context.InterestDistributions.FindAsync(id);
            return query!;
        }

        public async Task UpdateAsync(InterestDistribution distribution)
        {
            _context.InterestDistributions.Update(distribution);
            await _context.SaveChangesAsync();
        }
    }
}
