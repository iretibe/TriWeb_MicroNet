using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetAllAccountTerminationQueryHandler : IQueryHandler<GetAllAccountTerminationQuery, List<AccountTermination>>
    {
        private readonly IAccountTerminationRepository _repository;

        public GetAllAccountTerminationQueryHandler(IAccountTerminationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccountTermination>> Handle(GetAllAccountTerminationQuery request, CancellationToken cancellationToken)
            => await _repository.GetAllAsync();
    }
}
