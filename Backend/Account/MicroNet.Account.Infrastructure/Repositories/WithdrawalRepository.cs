using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Account.Infrastructure.Repositories
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        private readonly AccountContext _context;

        public WithdrawalRepository(AccountContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Withdrawal withdrawal)
        {
            await _context.Withdrawals.AddAsync(withdrawal);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid accountId)
            => await _context.Withdrawals.AnyAsync(x => x.Id == accountId);

        public async Task<List<Withdrawal>> GetAllAsync()
            => await _context.Withdrawals.ToListAsync();

        public async Task<Withdrawal> GetByIdAsync(Guid id)
        {
            var query = await _context.Withdrawals.FindAsync(id);
            return query ?? throw new KeyNotFoundException($"Withdrawal with ID {id} not found.");
        }
    }
}
