using MicroNet.Pos.Application.Exceptions;
using MicroNet.Pos.Application.Queries;
using MicroNet.Pos.Core.Dtos;
using MicroNet.Pos.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Handlers.Queries
{
    public class GetTransactionByAccountNumberQueryHandler : IQueryHandler<GetTransactionByAccountNumberQuery, TransactionDto>
    {
        private readonly ITransactionRepository _repository;

        public GetTransactionByAccountNumberQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransactionDto> Handle(GetTransactionByAccountNumberQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetByAccountNumberAsync(request.AccountNumber);

            if (transaction == null)
                throw new TransactionAccountNumberNotFoundException(request.AccountNumber);

            return new TransactionDto
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType,
                AccountNumber = transaction.AccountNumber,
                AccountName = transaction.AccountName,
                Reference = transaction.Reference,
                Amount = transaction.Amount,
                PaymentChannel = transaction.PaymentChannel,
                DepositorName = transaction.DepositorName,
                DepositorIdType = transaction.DepositorIdType,
                DepositorIdNumber = transaction.DepositorIdNumber,
                AgentCode = transaction.AgentCode,
                AgentPin = transaction.AgentPin,
                DestinationNetwork = transaction.DestinationNetwork,
                WalletNumber = transaction.WalletNumber,
                OTP = transaction.OTP,
                //CreatedBy = transaction.Crea
            };
        }
    }
}
