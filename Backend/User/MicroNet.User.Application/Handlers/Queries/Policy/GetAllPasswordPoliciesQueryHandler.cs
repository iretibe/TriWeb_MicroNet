using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Policy;
using MicroNet.User.Core.Dto.Policy;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Policy
{
    public class GetAllPasswordPoliciesQueryHandler : IQueryHandler<GetAllPasswordPoliciesQuery, IEnumerable<PasswordPolicyDto>>
    {
        private readonly IPasswordPolicyRepository _repository;

        public GetAllPasswordPoliciesQueryHandler(IPasswordPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PasswordPolicyDto>> Handle(GetAllPasswordPoliciesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPasswordPoliciesAsync();
        }
    }
}
