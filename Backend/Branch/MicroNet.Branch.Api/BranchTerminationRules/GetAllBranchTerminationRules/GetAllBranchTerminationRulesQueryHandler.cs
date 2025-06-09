using MicroNet.Branch.Api.Dtos;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.BranchTerminationRules.GetAllBranchTerminationRules
{
    public class GetAllBranchTerminationRulesQueryHandler : IQueryHandler<GetAllBranchTerminationRulesQuery, IList<BranchTerminationRuleDto>>
    {
        private readonly IBranchTerminationRuleRepository _repository;

        public GetAllBranchTerminationRulesQueryHandler(IBranchTerminationRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<BranchTerminationRuleDto>> Handle(GetAllBranchTerminationRulesQuery request, CancellationToken cancellationToken)
        {
            var rules = await _repository.GetAllAsync(cancellationToken);

            return rules.Select(rule => new BranchTerminationRuleDto
            {
                Id = rule.Id,
                Code = rule.Code,
                Name = rule.Name,
                Description = rule.Description,
                Note = rule.Notes
            }).ToList();
        }
    }
}
