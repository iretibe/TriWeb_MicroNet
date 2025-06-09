using MediatR;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.UpdateBranchTerminationRule
{
    public class UpdateBranchTerminationRuleCommandHandler : ICommandHandler<UpdateBranchTerminationRuleCommand>
    {
        private readonly IBranchTerminationRuleRepository _repository;

        public UpdateBranchTerminationRuleCommandHandler(IBranchTerminationRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateBranchTerminationRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _repository.GetByIdAsync(request.Id);

            if (rule == null)
                throw new BranchTerminationRuleIdNotFoundException(request.Id);

            rule.Update(request.Id, request.Name, request.Description, request.Notes, request.UpdatedBy);

            await _repository.Update(rule);

            return Unit.Value;
        }
    }
}
