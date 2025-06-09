using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Permission;
using MicroNet.User.Core.Dto.Permission;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Permission
{
    public class GetAllUserPermissionsQueryHandler : IQueryHandler<GetAllUserPermissionsQuery, IEnumerable<UserPermissionDto>>
    {
        private readonly IUserPermissionRepository _repository;

        public GetAllUserPermissionsQueryHandler(IUserPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserPermissionDto>> Handle(GetAllUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var allPermissions = await _repository.GetAllUserPermissionsAsync();

            return allPermissions;
        }
    }
}
