using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Exceptions.Permission;
using MicroNet.User.Application.Queries.Permission;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Permission
{
    public class GetUserPermissionsByIdQueryHandler : IQueryHandler<GetUserPermissionsByIdQuery, UserPermission>
    {
        private readonly IUserPermissionRepository _repository;

        public GetUserPermissionsByIdQueryHandler(IUserPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPermission> Handle(GetUserPermissionsByIdQuery request, CancellationToken cancellationToken)
        {
            var userPermission = await _repository.GetUserPermissionsByIdAsync(request.Id);

            if (userPermission == null) 
            { 
                throw new UserPermissionIdNotFoundException(request.Id);
            }

            return userPermission;
        }
    }
}
