namespace MicroNet.Client.Core.Repositories
{
    public interface IClientRepository
    {
        Task<Core.Entities.Client> GetByIdAsync(Guid id);
        Task<List<Core.Entities.Client>> GetAllAsync();
        Task AddAsync(Core.Entities.Client client);
        Task UpdateAsync(Core.Entities.Client client);
        Task DeleteAsync(Core.Entities.Client client);
        Task<bool> ExistsAsync(string email);
    }
}
