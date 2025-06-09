using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetAllUserGroupsQuery : IQuery<IEnumerable<UserGroupDto>>
    {
    }
}
