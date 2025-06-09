using MicroNet.Account.Core.Entities;

namespace MicroNet.Account.Core.Repositories
{
    public interface IAccountTerminationRepository
    {
        Task<AccountTermination> GetByIdAsync(Guid id);
        Task<AccountTermination> GetByAccountNumberAsync(string accountNumber);
        Task<List<AccountTermination>> GetAllAsync();
        Task AddAsync(AccountTermination termination);
        Task<bool> ExistsAsync(string accountNumber);
    }
}
