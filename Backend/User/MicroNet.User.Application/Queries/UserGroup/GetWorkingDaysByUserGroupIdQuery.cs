using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetWorkingDaysByUserGroupIdQuery : IQuery<List<GetWorkingDayDto>>
    {
        public Guid UserGroupId { get; set; }

        public GetWorkingDaysByUserGroupIdQuery(Guid userGroupId)
        {
            UserGroupId = userGroupId;
        }
    }
}
