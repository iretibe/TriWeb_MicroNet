using MicroNet.Revenue.Core.Entities;

namespace MicroNet.Revenue.Core.Repositories
{
    public interface IManagementFeeRepository
    {
        Task<ManagementFee> GetByIdAsync(Guid id);
        Task<IEnumerable<ManagementFee>> GetAllAsync();
        Task AddAsync(ManagementFee fee);
        Task UpdateAsync(ManagementFee fee);
        Task DeleteAsync(Guid id);
    }
}
