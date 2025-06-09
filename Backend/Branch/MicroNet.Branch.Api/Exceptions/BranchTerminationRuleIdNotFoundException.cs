namespace MicroNet.Branch.Api.Exceptions
{
    public class BranchTerminationRuleIdNotFoundException : AppException
    {
        public Guid Id { get; set; }

        public BranchTerminationRuleIdNotFoundException(Guid id) : base($"Branch termination rule with ID: {id} was not found!")
        {
            Id = id;
        }
    }
}
