using MicroNet.Loan.Core.Entities;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Loan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Loan.Infrastructure.Repositories
{
    public class LoanRequestRepository : ILoanRequestRepository
    {
        private readonly LoanContext _context;

        public LoanRequestRepository(LoanContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LoanRequest request)
        {
            await _context.LoanRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) 
            => await _context.LoanRequests.AnyAsync(x => x.Id == id);

        public async Task<List<LoanRequest>> GetAllAsync()
            => await _context.LoanRequests.ToListAsync();

        public async Task<LoanRequest> GetByIdAsync(Guid id)
        {
            var query = await _context.LoanRequests.FindAsync(id);
            return query!;
        }

        public async Task UpdateAsync(LoanRequest request)
        {
            _context.LoanRequests.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
