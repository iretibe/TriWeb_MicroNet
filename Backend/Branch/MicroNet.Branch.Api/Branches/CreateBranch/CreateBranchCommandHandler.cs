using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.CreateBranch
{
    public class CreateBranchCommandHandler : ICommandHandler<CreateBranchCommand, Guid>
    {
        private readonly IBranchRepository _repository;

        public CreateBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = new Entities.Branch(
                request.BranchCode,
                request.BranchName,
                request.Address,
                request.Region,
                request.SetupDate,
                request.BranchManagerName,
                request.CreatedBy
            );

            await _repository.AddBranchAsync(branch);

            return branch.Id;
        }
    }
}
