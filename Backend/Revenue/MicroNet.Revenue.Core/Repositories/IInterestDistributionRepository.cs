using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Core.Repositories
{
    public interface IInterestDistributionRepository
    {
        Task<InterestDistribution> GetByIdAsync(Guid id);
        Task<IEnumerable<InterestDistribution>> GetAllAsync();
        Task AddAsync(InterestDistribution distribution);
        Task UpdateAsync(InterestDistribution distribution);
        Task DeleteAsync(Guid id);
    }
}
