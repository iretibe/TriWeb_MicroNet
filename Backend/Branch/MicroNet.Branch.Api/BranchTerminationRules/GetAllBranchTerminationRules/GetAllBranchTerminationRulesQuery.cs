using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.BranchTerminationRules.GetAllBranchTerminationRules
{
    public class GetAllBranchTerminationRulesQuery : IQuery<List<BranchTerminationRuleDto>> { }
}
