using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetAccountTransferByIdQueryHandler : IQueryHandler<GetAccountTransferByIdQuery, AccountTransfer>
    {
        private readonly IAccountTransferRepository _repository;

        public GetAccountTransferByIdQueryHandler(IAccountTransferRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountTransfer> Handle(GetAccountTransferByIdQuery request, CancellationToken cancellationToken)
            => await _repository.GetByIdAsync(request.Id);
    }
}
