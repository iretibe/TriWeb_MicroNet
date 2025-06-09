using MicroNet.Pos.Application.Exceptions;
using MicroNet.Pos.Application.Queries;
using MicroNet.Pos.Core.Dtos;
using MicroNet.Pos.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Pos.Application.Handlers.Queries
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
            var transaction = await _repository.GetByIdAsync(request.Id);

            if (transaction == null) 
                throw new TransactionIdNotFoundException(request.Id);

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
