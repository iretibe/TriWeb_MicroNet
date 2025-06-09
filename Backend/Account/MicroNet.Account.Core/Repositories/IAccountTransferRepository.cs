using MicroNet.Account.Core.Entities;

namespace MicroNet.Account.Core.Repositories
{
    public interface IAccountTransferRepository
    {
        Task<AccountTransfer> GetByIdAsync(Guid id);
        Task<List<AccountTransfer>> GetAllAsync();
        Task AddAsync(AccountTransfer transfer);
        Task<bool> ExistsAsync(Guid accountId);
    }
}
