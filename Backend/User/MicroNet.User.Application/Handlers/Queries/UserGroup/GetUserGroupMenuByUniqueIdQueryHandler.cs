using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetUserGroupMenuByUniqueIdQueryHandler : IQueryHandler<GetUserGroupMenuByUniqueIdQuery, IEnumerable<UserGroupMenu>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetUserGroupMenuByUniqueIdQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<IEnumerable<UserGroupMenu>> Handle(GetUserGroupMenuByUniqueIdQuery request, CancellationToken cancellationToken)
        {
            return await _userGroupRepository.GetUserGroupMenuByUniqueIdAsync(request.UserGroupId);
        }
    }
}
