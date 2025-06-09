using MicroNet.Product.Core.Entities;

namespace MicroNet.Product.Core.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(Guid id);
        Task<List<Loan>> GetAllAsync();
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(Loan loan);
        Task<bool> ExistsAsync(string loanCode);
    }
}
