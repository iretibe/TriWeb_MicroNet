namespace MicroNet.Branch.Api.Repositories
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Entities.Branch>> GetAllBranchesAsync();
        Task<Entities.Branch> GetBranchByIdAsync(Guid id);
        Task AddBranchAsync(Entities.Branch entity);
        Task UpdateBranchAsync(Entities.Branch entity);
        Task DeleteBranchAsync(Entities.Branch branch);
        Task<bool> CheckBranchNameExistsAsync(string branchName);
        Task<string> GetBranchNameByIdAsync(Guid id);
    }
}
