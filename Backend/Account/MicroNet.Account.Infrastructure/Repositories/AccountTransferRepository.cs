using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Account.Infrastructure.Repositories
{
    public class AccountTransferRepository : IAccountTransferRepository
    {
        private readonly AccountContext _context;

        public AccountTransferRepository(AccountContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AccountTransfer transfer)
        {
            await _context.AccountTransfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid accountId)
            => await _context.AccountTransfers.AnyAsync(x => x.Id == accountId);

        public async Task<List<AccountTransfer>> GetAllAsync()
            => await _context.AccountTransfers.ToListAsync();

        public async Task<AccountTransfer> GetByIdAsync(Guid id)
        {
            var query = await _context.AccountTransfers.FindAsync(id);

            return query ?? throw new KeyNotFoundException($"AccountTransfer with ID {id} not found.");
        }
    }
}
