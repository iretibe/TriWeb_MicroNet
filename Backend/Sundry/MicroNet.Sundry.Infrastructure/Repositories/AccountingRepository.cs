using MicroNet.Sundry.Core.Entities;
using MicroNet.Sundry.Core.Repositories;
using MicroNet.Sundry.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Sundry.Infrastructure.Repositories
{
    public class AccountingRepository : IAccountingRepository
    {
        private readonly AccountingContext _context;

        public AccountingRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Accounting account)
        {
            await _context.Accountings.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string code)
            => await _context.Accountings.AnyAsync(x => x.Code == code);

        public async Task<List<Accounting>> GetAllAsync()
            => await _context.Accountings.ToListAsync();

        public async Task<Accounting?> GetByIdAsync(Guid id)
            => await _context.Accountings.FindAsync(id);
    }
}
