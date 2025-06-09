using MicroNet.Pos.Core.Entities;
using MicroNet.Pos.Core.Repositories;
using MicroNet.Pos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Pos.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PosContext _context;

        public TransactionRepository(PosContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Transactions.AnyAsync(t => t.Id == id);
        }

        public async Task<List<Transaction>> GetAllAsync()
            => await _context.Transactions.ToListAsync();

        public async Task<Transaction> GetByAccountNumberAsync(string accountNumber)
        {
            var query = await _context.Transactions.Where(t => t.AccountName == accountNumber).FirstOrDefaultAsync();
            return query!;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var query = await _context.Transactions.FindAsync(id);
            return query!;
        }
    }
}
