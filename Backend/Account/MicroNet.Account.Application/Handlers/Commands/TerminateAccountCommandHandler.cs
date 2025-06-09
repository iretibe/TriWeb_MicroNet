using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Events;
using MicroNet.Account.Application.Exceptions;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Events;
using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Handlers.Commands
{
    public class TerminateAccountCommandHandler : ICommandHandler<TerminateAccountCommand, Guid>
    {
        private readonly IAccountTerminationRepository _repository;

        public TerminateAccountCommandHandler(IAccountTerminationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(TerminateAccountCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.ExistsAsync(request.AccountNumber))
                throw new AccountIdAlreadyTerminatedException(request.AccountNumber);

            var accountTermination = await _repository.GetByAccountNumberAsync(request.AccountNumber);

            var accountDetails = new AccountDetails(
                request.AccountNumber,
                accountTermination.TerminatedAccount.AccountName,
                accountTermination.TerminatedAccount.Balance,
                accountTermination.TerminatedAccount.BranchName
            );

            var termination = new AccountTermination(accountDetails, request.Reason, request.TerminatedBy);
            await _repository.AddAsync(termination);

            //Raise domain event (optional)
            DomainEvents.Raise(new AccountTerminatedEvent(request.AccountNumber));

            return termination.Id;
        }
    }
}
