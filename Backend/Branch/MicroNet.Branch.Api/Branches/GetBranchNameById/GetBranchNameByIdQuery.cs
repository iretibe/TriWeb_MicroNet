using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.Branches.GetBranchNameById
{
    public class GetBranchNameByIdQuery : IQuery<string>
    {
        public Guid BranchId { get; set; }

        public GetBranchNameByIdQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }
}
