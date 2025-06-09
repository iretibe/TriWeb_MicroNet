using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Core.Repositories
{
    public interface IPenaltyChargeRepository
    {
        Task<PenaltyCharge> GetByIdAsync(Guid id);
        Task<IEnumerable<PenaltyCharge>> GetAllAsync();
        Task AddAsync(PenaltyCharge charge);
        Task UpdateAsync(PenaltyCharge charge);
        Task DeleteAsync(Guid id);
    }
}
