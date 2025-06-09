using MicroNet.Branch.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Branch.Api.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly BranchContext _context;

        public BranchRepository(BranchContext context)
        {
            _context = context;
        }

        public async Task AddBranchAsync(Entities.Branch entity)
        {
            _context.Branches.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckBranchNameExistsAsync(string BranchName)
        {
            return await _context.Branches.AnyAsync(b => b.BranchName == BranchName);
        }

        public async Task DeleteBranchAsync(Entities.Branch branch)
        {
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Branch>> GetAllBranchesAsync()
        {
            return await _context.Branches
                .Include(b => b.ProductSummaries)
                .ToListAsync();
        }

        public async Task<Entities.Branch> GetBranchByIdAsync(Guid id)
        {
            var query = await _context.Branches
                .Include(b => b.ProductSummaries)
                .FirstOrDefaultAsync(b => b.Id == id);

            return query ?? throw new KeyNotFoundException($"Branch with ID '{id}' not found.");
        }

        public async Task<string> GetBranchNameByIdAsync(Guid id)
        {
            var result = await _context
                .Branches.Where(b => b.Id == id).FirstOrDefaultAsync();

            return result!.BranchName;
        }

        public async Task UpdateBranchAsync(Entities.Branch entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
