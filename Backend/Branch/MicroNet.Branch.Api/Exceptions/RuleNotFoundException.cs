namespace MicroNet.Branch.Api.Exceptions
{
    public class RuleNotFoundException : AppException
    {
        public RuleNotFoundException(Guid id) : base($"Branch Termination Rule with ID: `{id}` was not found")
        {
        }
    }
}
