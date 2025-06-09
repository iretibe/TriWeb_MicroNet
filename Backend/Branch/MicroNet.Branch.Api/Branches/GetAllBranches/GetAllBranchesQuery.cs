using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.Branches.GetAllBranches
{
    public class GetAllBranchesQuery : IQuery<IEnumerable<BranchDto>>
    {
    }
}
