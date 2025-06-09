using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetUserGroupByNameQueryHandler : IQueryHandler<GetUserGroupByNameQuery, Core.Entities.UserGroup>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupByNameQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<Core.Entities.UserGroup> Handle(GetUserGroupByNameQuery request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetUserGroupByNameAsync(request.UserGroupName, request.BranchId);
        }
    }
}
