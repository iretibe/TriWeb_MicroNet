using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Policy;

namespace MicroNet.User.Application.Queries.Policy
{
    public class GetPasswordPolicyByIdQuery : IQuery<PasswordPolicyDto>
    {
        public Guid Id { get; set; }

        public GetPasswordPolicyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
