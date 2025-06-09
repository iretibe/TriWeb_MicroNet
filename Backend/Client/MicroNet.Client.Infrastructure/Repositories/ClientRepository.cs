using MicroNet.Client.Core.Repositories;
using MicroNet.Client.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Client.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;

        public ClientRepository(ClientContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Core.Entities.Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Core.Entities.Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string email)
            => await _context.Clients.AnyAsync(c => c.Email == email);

        public async Task<List<Core.Entities.Client>> GetAllAsync()
            => await _context.Clients.ToListAsync();

        public async Task<Core.Entities.Client> GetByIdAsync(Guid id)
            => await _context.Clients.Where(c => c.Id == id).SingleOrDefaultAsync();

        public async Task UpdateAsync(Core.Entities.Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
