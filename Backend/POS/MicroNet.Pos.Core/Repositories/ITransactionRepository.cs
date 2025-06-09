using MicroNet.Pos.Core.Entities;

namespace MicroNet.Pos.Core.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<Transaction> GetByAccountNumberAsync(string accountNumber);
        Task<List<Transaction>> GetAllAsync();
        Task AddAsync(Transaction transaction);
        Task<bool> ExistsAsync(Guid id);
    }
}
