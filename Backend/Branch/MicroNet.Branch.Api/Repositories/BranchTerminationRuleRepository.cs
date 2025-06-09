using MicroNet.Branch.Api.Data;
using MicroNet.Branch.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Branch.Api.Repositories
{
    public class BranchTerminationRuleRepository : IBranchTerminationRuleRepository
    {
        private readonly BranchContext _context;

        public BranchTerminationRuleRepository(BranchContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BranchTerminationRule rule, CancellationToken cancellationToken = default)
        {
            await _context.BranchTerminationRules.AddAsync(rule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(BranchTerminationRule entity)
        {
            _context.BranchTerminationRules.Remove(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<BranchTerminationRule>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BranchTerminationRules.ToListAsync(cancellationToken);
        }

        public async Task<BranchTerminationRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.BranchTerminationRules
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task Update(BranchTerminationRule entity)
        {
            _context.BranchTerminationRules.Update(entity);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
