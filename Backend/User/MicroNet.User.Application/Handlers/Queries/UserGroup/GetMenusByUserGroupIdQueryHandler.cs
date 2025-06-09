using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetMenusByUserGroupIdQueryHandler : IQueryHandler<GetMenusByUserGroupIdQuery, List<GetMenuDto>>
    {
        private readonly IUserGroupRepository _repository;

        public GetMenusByUserGroupIdQueryHandler(IUserGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetMenuDto>> Handle(GetMenusByUserGroupIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMenusByUserGroupIdAsync(request.UserGroupId);
        }
    }
}
