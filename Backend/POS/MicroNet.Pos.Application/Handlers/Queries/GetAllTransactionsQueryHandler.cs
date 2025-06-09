using MicroNet.Pos.Application.Exceptions;
using MicroNet.Pos.Application.Queries;
using MicroNet.Pos.Core.Dtos;
using MicroNet.Pos.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Handlers.Queries
{
    public class GetAllTransactionsQueryHandler : IQueryHandler<GetAllTransactionsQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _repository;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetAllAsync();

            if (transaction == null)
                throw new TransactionNotFoundException();

            return transaction.Select(t => new TransactionDto
            {
                Id = t.Id,
                TransactionType = t.TransactionType,
                AccountNumber = t.AccountNumber,
                AccountName = t.AccountName,
                Reference = t.Reference,
                Amount = t.Amount,
                PaymentChannel = t.PaymentChannel,
                DepositorName = t.DepositorName,
                DepositorIdType = t.DepositorIdType,
                DepositorIdNumber = t.DepositorIdNumber,
                AgentCode = t.AgentCode,
                AgentPin = t.AgentPin,
                DestinationNetwork = t.DestinationNetwork,
                WalletNumber = t.WalletNumber,
                OTP = t.OTP,
                //CreatedBy = transaction.Crea
            }).ToList();
        }
    }
}
