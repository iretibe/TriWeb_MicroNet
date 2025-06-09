using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.DeleteBranchTerminationRule
{
    public class DeleteBranchTerminationRuleCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteBranchTerminationRuleCommand(Guid id)
        {
            Id = id;
        }
    }
}
