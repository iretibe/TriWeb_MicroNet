namespace MicroNet.Branch.Api.Exceptions
{
    public class BranchNotFoundException : AppException
    {
        public BranchNotFoundException(Guid id) : base($"Branch with ID: `{id}` was not found.")
        {
        }
    }
}
