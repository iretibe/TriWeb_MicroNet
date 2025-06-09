using MicroNet.Branch.Api.Entities;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.CreateBranchTerminationRule
{
    public class CreateBranchTerminationRuleCommandHandler : ICommandHandler<CreateBranchTerminationRuleCommand, Guid>
    {
        private readonly IBranchTerminationRuleRepository _repository;

        public CreateBranchTerminationRuleCommandHandler(IBranchTerminationRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateBranchTerminationRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = new BranchTerminationRule(
                request.Code,
                request.Name,
                request.Description,
                request.Notes,
                request.CreatedBy
            );

            await _repository.AddAsync(rule);

            return rule.Id;
        }
    }
}
