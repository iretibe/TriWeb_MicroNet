using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetAllUserGroupsQueryHandler : IQueryHandler<GetAllUserGroupsQuery, IEnumerable<UserGroupDto>>
    {
        private readonly IUserGroupRepository _repository;

        public GetAllUserGroupsQueryHandler(IUserGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserGroupDto>> Handle(GetAllUserGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllUserGroupsAsync();
        }
    }
}
