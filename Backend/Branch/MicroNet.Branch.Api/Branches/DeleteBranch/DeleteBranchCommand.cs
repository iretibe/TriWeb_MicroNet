using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.DeleteBranch
{
    public class DeleteBranchCommand : ICommand
    {
        public Guid BranchId { get; set; }

        public DeleteBranchCommand(Guid branchId)
        {
            BranchId = branchId;
        }
    }
}
