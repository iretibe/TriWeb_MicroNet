using MicroNet.Account.Core.Entities;

namespace MicroNet.Account.Core.Repositories
{
    public interface IWithdrawalRepository
    {
        Task<Withdrawal> GetByIdAsync(Guid id);
        Task<List<Withdrawal>> GetAllAsync();
        Task AddAsync(Withdrawal withdrawal);
        Task<bool> ExistsAsync(Guid accountId);
    }
}
