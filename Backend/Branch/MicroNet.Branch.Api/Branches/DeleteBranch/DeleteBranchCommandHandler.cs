using MediatR;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.DeleteBranch
{
    public class DeleteBranchCommandHandler : ICommandHandler<DeleteBranchCommand>
    {
        private readonly IBranchRepository _repository;

        public DeleteBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _repository.GetBranchByIdAsync(request.BranchId);

            if (branch is null)
                throw new BranchNotFoundException(request.BranchId);

            await _repository.DeleteBranchAsync(branch);

            return Unit.Value;
        }
    }
}
