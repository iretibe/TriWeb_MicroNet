using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Account.Infrastructure.Repositories
{
    public class AccountTerminationRepository : IAccountTerminationRepository
    {
        private readonly AccountContext _context;

        public AccountTerminationRepository(AccountContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AccountTermination termination)
        {
            await _context.AccountTerminations.AddAsync(termination);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string accountNumber)
            => await _context.AccountTerminations.AnyAsync(x => x.TerminatedAccount.AccountNumber == accountNumber);

        public async Task<List<AccountTermination>> GetAllAsync()
            => await _context.AccountTerminations.ToListAsync();

        public async Task<AccountTermination> GetByAccountNumberAsync(string accountNumber)
        {
            var termination = await _context.AccountTerminations
                .FirstOrDefaultAsync(x => x.TerminatedAccount.AccountNumber == accountNumber);

            return termination ?? throw new KeyNotFoundException($"Account termination with account number '{accountNumber}' not found.");
        }

        public async Task<AccountTermination> GetByIdAsync(Guid id) 
            => await _context.AccountTerminations.FindAsync(id);
    }
}
