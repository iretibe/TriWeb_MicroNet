using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetUserGroupMenuByUniqueIdQuery : IQuery<IEnumerable<UserGroupMenu>>
    {
        public Guid UserGroupId { get; set; }

        public GetUserGroupMenuByUniqueIdQuery(Guid userGroupId)
        {
            UserGroupId = userGroupId;
        }
    }
}
