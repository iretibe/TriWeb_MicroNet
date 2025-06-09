using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _repository;

        public CreateTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.transactionDto;

            var transaction = new Transaction(
                Guid.NewGuid(),
                new ClientAccount(dto.AccountNumber, dto.AccountName),
                dto.Amount,
                dto.Reference,
                new DepositorId(dto.IdType, dto.IdNumber),
                dto.DepositorName,
                dto.DestinationType,
                dto.CreatedBy
            );

            await _repository.AddAsync(transaction);
            return transaction.Id;
        }
    }
}
