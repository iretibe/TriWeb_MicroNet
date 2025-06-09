using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Core.Repositories
{
    public interface IRevenueReversalRepository
    {
        Task<RevenueReversal> GetByIdAsync(Guid id);
        Task<IEnumerable<RevenueReversal>> GetAllAsync();
        Task AddAsync(RevenueReversal reversal);
        Task UpdateAsync(RevenueReversal reversal);
        Task DeleteAsync(Guid id);
    }
}
