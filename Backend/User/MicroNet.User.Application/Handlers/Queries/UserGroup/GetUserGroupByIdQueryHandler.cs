using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetUserGroupByIdQueryHandler : IQueryHandler<GetUserGroupByIdQuery, GetUserGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupByIdQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<GetUserGroupDto> Handle(GetUserGroupByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetUserGroupByIdAsync(request.UserGroupId);
        }
    }
}
