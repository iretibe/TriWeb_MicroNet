using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Policy;
using MicroNet.User.Core.Dto.Policy;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Policy
{
    public class GetPasswordPolicyByIdQueryHandler : IQueryHandler<GetPasswordPolicyByIdQuery, PasswordPolicyDto>
    {
        private readonly IPasswordPolicyRepository _repository;

        public GetPasswordPolicyByIdQueryHandler(IPasswordPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PasswordPolicyDto> Handle(GetPasswordPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPasswordPolicyByIdAsync(request.Id);
        }
    }
}
