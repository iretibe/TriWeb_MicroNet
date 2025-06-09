using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.BranchTerminationRules.GetBranchTerminationRuleById
{
    public class GetBranchTerminationRuleByIdQuery : IQuery<BranchTerminationRuleDto>
    {
        public Guid Id { get; set; }

        public GetBranchTerminationRuleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
