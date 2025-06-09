using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetUserGroupByIdQuery : IQuery<GetUserGroupDto>
    {
        public Guid UserGroupId { get; set; }

        public GetUserGroupByIdQuery(Guid userGroupId)
        {
            UserGroupId = userGroupId;
        }
    }
}
