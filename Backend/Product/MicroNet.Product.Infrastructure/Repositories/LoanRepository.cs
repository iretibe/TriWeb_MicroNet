using MicroNet.Product.Core.Entities;
using MicroNet.Product.Core.Repositories;
using MicroNet.Product.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Product.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ProductContext _context;

        public LoanRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string loanCode)
        {
            return await _context.Loans.AnyAsync(l => l.LoanCode == loanCode);
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(Guid id)
        {
            return await _context.Loans.FindAsync(id) ?? throw new KeyNotFoundException($"Loan with ID {id} not found.");
        }

        public async Task UpdateAsync(Loan loan)
        {
            var existingLoan = await _context.Loans.FindAsync(loan.Id);
            if (existingLoan == null)
            {
                throw new KeyNotFoundException($"Loan with ID {loan.Id} not found.");
            }
            
            existingLoan.Update(loan.Id, loan.LoanName, loan.MaximumAmount, loan.PercentageOfSavings,
                loan.InterestRate, loan.RepaymentPeriod, loan.GracePeriod, loan.AuditInfo.UpdatedBy!);
            
            await _context.SaveChangesAsync();
        }
    }
}
