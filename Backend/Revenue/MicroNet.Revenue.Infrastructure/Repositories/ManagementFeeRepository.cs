using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Revenue.Infrastructure.Repositories
{
    public class ManagementFeeRepository : IManagementFeeRepository
    {
        private readonly RevenueContext _context;

        public ManagementFeeRepository(RevenueContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ManagementFee fee)
        {
            _context.ManagementFees.Add(fee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = await _context.ManagementFees.FindAsync(id);
            if (query != null)
            {
                _context.ManagementFees.Remove(query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ManagementFee>> GetAllAsync()
        {
            return await _context.ManagementFees.ToListAsync();
        }

        public async Task<ManagementFee> GetByIdAsync(Guid id)
        {
            var query = await _context.ManagementFees.FindAsync(id);
            return query!;
        }

        public async Task UpdateAsync(ManagementFee fee)
        {
            _context.ManagementFees.Update(fee);
            await _context.SaveChangesAsync();
        }
    }
}
