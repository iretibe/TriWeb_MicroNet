using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Events;
using MicroNet.Account.Application.Exceptions;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Events;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Handlers.Commands
{
    public class InitiateWithdrawalCommandHandler : ICommandHandler<InitiateWithdrawalCommand, Guid>
    {
        private readonly IWithdrawalRepository _repository;

        public InitiateWithdrawalCommandHandler(IWithdrawalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(InitiateWithdrawalCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                throw new InvalidWithdrawalAmountException(request.Amount);

            var withdrawal = new Withdrawal(request.AccountNumber, request.Amount, request.Reference, request.PaymentMode, request.RequestedBy);
            await _repository.AddAsync(withdrawal);

            //Raise domain event (optional)
            DomainEvents.Raise(new WithdrawalInitiatedEvent(withdrawal.Id));

            return withdrawal.Id;
        }
    }
}
