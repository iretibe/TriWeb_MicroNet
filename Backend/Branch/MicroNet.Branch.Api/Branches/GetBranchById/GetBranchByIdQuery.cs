using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.Branches.GetBranchById
{
    public class GetBranchByIdQuery : IQuery<BranchDto>
    {
        public Guid BranchId { get; set; }

        public GetBranchByIdQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }
}
