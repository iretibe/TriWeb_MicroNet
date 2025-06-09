using MicroNet.Loan.Core.Entities;

namespace MicroNet.Loan.Core.Repositories
{
    public interface ILoanRequestRepository
    {
        Task<LoanRequest> GetByIdAsync(Guid id);
        Task<List<LoanRequest>> GetAllAsync();
        Task AddAsync(LoanRequest request);
        Task UpdateAsync(LoanRequest request);
        Task<bool> ExistsAsync(Guid id);
    }
}
