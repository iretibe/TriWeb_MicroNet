using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Revenue.Infrastructure.Repositories
{
    public class PenaltyChargeRepository : IPenaltyChargeRepository
    {
        private readonly RevenueContext _context;

        public PenaltyChargeRepository(RevenueContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PenaltyCharge charge)
        {
            _context.PenaltyCharges.Add(charge);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = await _context.PenaltyCharges.FindAsync(id);
            if (query != null)
            {
                _context.PenaltyCharges.Remove(query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PenaltyCharge>> GetAllAsync()
        {
            return await _context.PenaltyCharges.ToListAsync();
        }

        public async Task<PenaltyCharge> GetByIdAsync(Guid id)
        {
            var query = await _context.PenaltyCharges.FindAsync(id);
            return query!;
        }

        public async Task UpdateAsync(PenaltyCharge charge)
        {
            _context.PenaltyCharges.Update(charge);
            await _context.SaveChangesAsync();
        }
    }
}
