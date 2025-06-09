using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetWithdrawalByIdQueryHandler : IQueryHandler<GetWithdrawalByIdQuery, Withdrawal>
    {
        private readonly IWithdrawalRepository _repository;

        public GetWithdrawalByIdQueryHandler(IWithdrawalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Withdrawal> Handle(GetWithdrawalByIdQuery request, CancellationToken cancellationToken)
            => await _repository.GetByIdAsync(request.Id);
    }
}
