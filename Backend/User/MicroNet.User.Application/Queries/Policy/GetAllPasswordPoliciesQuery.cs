using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Policy;

namespace MicroNet.User.Application.Queries.Policy
{
    public class GetAllPasswordPoliciesQuery : IQuery<IEnumerable<PasswordPolicyDto>>
    {
    }
}
