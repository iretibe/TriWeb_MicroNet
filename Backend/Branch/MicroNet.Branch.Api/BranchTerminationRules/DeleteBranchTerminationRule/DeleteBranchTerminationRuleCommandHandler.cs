using MediatR;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.DeleteBranchTerminationRule
{
    public class DeleteBranchTerminationRuleCommandHandler : ICommandHandler<DeleteBranchTerminationRuleCommand>
    {
        private readonly IBranchTerminationRuleRepository _repository;

        public DeleteBranchTerminationRuleCommandHandler(IBranchTerminationRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBranchTerminationRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _repository.GetByIdAsync(request.Id);

            if (rule is null)
                throw new RuleNotFoundException(request.Id);

            await _repository.Delete(rule);

            return Unit.Value;
        }
    }
}
