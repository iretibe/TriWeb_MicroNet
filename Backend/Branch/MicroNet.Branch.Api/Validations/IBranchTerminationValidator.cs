namespace MicroNet.Branch.Api.Validations
{
    public interface IBranchTerminationValidator
    {
        Task<List<string>> ValidateAsync(Guid branchId);
    }
}
