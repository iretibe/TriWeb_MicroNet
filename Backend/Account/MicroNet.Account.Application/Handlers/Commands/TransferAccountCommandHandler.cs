using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Events;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Events;
using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Handlers.Commands
{
    public class TransferAccountCommandHandler : ICommandHandler<TransferAccountCommand, Guid>
    {
        private readonly IAccountTransferRepository _repository;

        public TransferAccountCommandHandler(IAccountTransferRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(TransferAccountCommand request, CancellationToken cancellationToken)
        {
            var accountDetails = new AccountDetails(
                request.AccountNumber,
                request.AccountName,
                request.Balance,
                request.CurrentBranch
            );

            var transfer = new AccountTransfer(accountDetails, request.ToBranchName, request.TransferredBy);
            await _repository.AddAsync(transfer);

            //Raise domain event (optional)
            DomainEvents.Raise(new AccountTransferredEvent(request.AccountNumber));

            return transfer.Id;
        }
    }
}
