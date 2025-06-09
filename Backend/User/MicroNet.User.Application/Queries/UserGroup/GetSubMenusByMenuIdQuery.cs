using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetSubMenusByMenuIdQuery : IQuery<List<GetSubMenuDto>>
    {
        public Guid UserGroupId { get; set; }
        public Guid MenuId { get; set; }

        public GetSubMenusByMenuIdQuery(Guid userGroupId, Guid menuId)
        {
            UserGroupId = userGroupId;
            MenuId = menuId;
        }
    }
}
