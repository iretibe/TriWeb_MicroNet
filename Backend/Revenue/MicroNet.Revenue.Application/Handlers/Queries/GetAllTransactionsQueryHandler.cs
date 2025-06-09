using MediatR;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _repository;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var txns = await _repository.GetAllAsync();

            return txns.Select(txn => new TransactionDto
            {
                Id = txn.Id,
                AccountNumber = txn.Receiver.AccountNumber,
                AccountName = txn.Receiver.AccountName,
                Amount = txn.Amount,
                Reference = txn.Reference,
                DepositorName = txn.DepositorName,
                DestinationType = txn.DestinationType,
                CreatedAt = txn.AuditInfo.CreatedAt
            }).ToList();
        }
    }
}
