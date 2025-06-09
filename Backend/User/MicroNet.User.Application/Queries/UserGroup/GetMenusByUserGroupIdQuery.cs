using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetMenusByUserGroupIdQuery : IQuery<List<GetMenuDto>>
    {
        public Guid UserGroupId { get; set; }

        public GetMenusByUserGroupIdQuery(Guid userGroupId)
        {
            UserGroupId = userGroupId;
        }
    }
}
