using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetSubMenusByMenuIdQueryHandler : IQueryHandler<GetSubMenusByMenuIdQuery, List<GetSubMenuDto>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetSubMenusByMenuIdQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<List<GetSubMenuDto>> Handle(GetSubMenusByMenuIdQuery request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetSubMenusByMenuIdAsync(request.UserGroupId, request.MenuId);
        }
    }
}
