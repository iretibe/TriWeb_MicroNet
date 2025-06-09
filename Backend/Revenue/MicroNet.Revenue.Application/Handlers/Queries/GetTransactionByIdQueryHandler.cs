using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _repository;

        public GetTransactionByIdQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var txn = await _repository.GetByIdAsync(request.Id);
            if (txn == null) throw new TransactionIdNotFoundException(request.Id);

            return new TransactionDto
            {
                Id = txn.Id,
                AccountNumber = txn.Receiver.AccountNumber,
                AccountName = txn.Receiver.AccountName,
                Amount = txn.Amount,
                Reference = txn.Reference,
                DepositorName = txn.DepositorName,
                DestinationType = txn.DestinationType,
                CreatedAt = txn.AuditInfo.CreatedAt
            };
        }
    }
}
