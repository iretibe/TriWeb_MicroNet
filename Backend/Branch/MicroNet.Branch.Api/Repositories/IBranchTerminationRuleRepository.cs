using MicroNet.Branch.Api.Entities;

namespace MicroNet.Branch.Api.Repositories
{
    public interface IBranchTerminationRuleRepository
    {
        Task<BranchTerminationRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<BranchTerminationRule>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(BranchTerminationRule rule, CancellationToken cancellationToken = default);
        Task Update(BranchTerminationRule entity);
        Task Delete(BranchTerminationRule entity);
    }
}
