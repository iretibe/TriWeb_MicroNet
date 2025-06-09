using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetAllAccountTransferQueryHandler : IQueryHandler<GetAllAccountTransferQuery, List<AccountTransfer>>
    {
        private readonly IAccountTransferRepository _repository;

        public GetAllAccountTransferQueryHandler(IAccountTransferRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccountTransfer>> Handle(GetAllAccountTransferQuery request, CancellationToken cancellationToken)
            => await _repository.GetAllAsync();
    }
}
