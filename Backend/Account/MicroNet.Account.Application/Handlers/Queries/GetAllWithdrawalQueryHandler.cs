using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetAllWithdrawalQueryHandler : IQueryHandler<GetAllWithdrawalQuery, List<Withdrawal>>
    {
        private readonly IWithdrawalRepository _repository;

        public GetAllWithdrawalQueryHandler(IWithdrawalRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Withdrawal>> Handle(GetAllWithdrawalQuery request, CancellationToken cancellationToken)
            => await _repository.GetAllAsync();
    }
}
