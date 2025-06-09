using MicroNet.Sundry.Core.Entities;

namespace MicroNet.Sundry.Core.Repositories
{
    public interface IAccountingRepository
    {
        Task<Accounting?> GetByIdAsync(Guid id);
        Task<List<Accounting>> GetAllAsync();
        Task AddAsync(Accounting account);
        Task<bool> ExistsAsync(string code);
    }
}
