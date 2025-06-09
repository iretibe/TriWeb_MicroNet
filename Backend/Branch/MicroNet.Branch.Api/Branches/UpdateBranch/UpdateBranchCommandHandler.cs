using MediatR;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.UpdateBranch
{
    public class UpdateBranchCommandHandler : ICommandHandler<UpdateBranchCommand>
    {
        private readonly IBranchRepository _repository;

        public UpdateBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _repository.GetBranchByIdAsync(request.Id);

            if (branch is null)
                throw new BranchNotFoundException(request.Id);

            branch.UpdateManager(request.Id, branch.BranchName, branch.PhysicalAddress,
                branch.Region, branch.SetupDate, branch.ManagerName, branch.AuditInfo.UpdatedBy!);

            await _repository.UpdateBranchAsync(branch);

            return Unit.Value;
        }
    }
}
