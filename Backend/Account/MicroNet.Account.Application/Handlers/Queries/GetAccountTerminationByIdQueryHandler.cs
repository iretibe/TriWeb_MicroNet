using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Account.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Handlers.Queries
{
    public class GetAccountTerminationByIdQueryHandler : IQueryHandler<GetAccountTerminationByIdQuery, AccountTermination>
    {
        private readonly IAccountTerminationRepository _repository;

        public GetAccountTerminationByIdQueryHandler(IAccountTerminationRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountTermination> Handle(GetAccountTerminationByIdQuery request, CancellationToken cancellationToken)
            => await _repository.GetByIdAsync(request.Id);
    }
}
