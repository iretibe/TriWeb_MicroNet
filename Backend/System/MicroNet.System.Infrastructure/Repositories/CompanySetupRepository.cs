using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Repositories;
using MicroNet.System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.System.Infrastructure.Repositories
{
    public class CompanySetupRepository : ICompanySetupRepository
    {
        private readonly SystemContext _context;

        public CompanySetupRepository(SystemContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CompanySetup entity)
        {
            await _context.CompanySetups.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var setup = await _context.CompanySetups.FindAsync(id);
            if (setup != null)
            {
                _context.CompanySetups.Remove(setup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string companyName)
        {
            return await _context.CompanySetups
                .AsNoTracking()
                .AnyAsync(c => c.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<CompanySetup>> GetAllAsync()
        {
            return await _context.CompanySetups.AsNoTracking().ToListAsync();
        }

        public async Task<CompanySetup?> GetByIdAsync(Guid id)
        {
            return await _context.CompanySetups
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(CompanySetup entity)
        {
            _context.CompanySetups.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
