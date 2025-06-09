using MicroNet.System.Core.Entities;

namespace MicroNet.System.Core.Repositories
{
    public interface ICompanySetupRepository
    {
        Task<CompanySetup?> GetByIdAsync(Guid id);
        Task<IEnumerable<CompanySetup>> GetAllAsync();
        Task AddAsync(CompanySetup entity);
        Task UpdateAsync(CompanySetup entity);
        Task<bool> ExistsAsync(string companyName);
        Task DeleteAsync(Guid id);
    }
}
