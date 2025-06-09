using MicroNet.Branch.Api.Dtos;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.BranchTerminationRules.GetBranchTerminationRuleById
{
    public class GetBranchTerminationRuleByIdQueryHandler : IQueryHandler<GetBranchTerminationRuleByIdQuery, BranchTerminationRuleDto>
    {
        private readonly IBranchTerminationRuleRepository _repository;

        public GetBranchTerminationRuleByIdQueryHandler(IBranchTerminationRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<BranchTerminationRuleDto> Handle(GetBranchTerminationRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var rule = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (rule == null)
            {
                throw new BranchTerminationRuleIdNotFoundException(request.Id);
            }

            return new BranchTerminationRuleDto
            {
                Id = rule!.Id,
                Code = rule.Code,
                Name = rule.Name,
                Description = rule.Description,
                Note = rule.Notes
            };
        }
    }
}
