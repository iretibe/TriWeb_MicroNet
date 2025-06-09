using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Permission;

namespace MicroNet.User.Application.Queries.Permission
{
    public class GetAllUserPermissionsQuery : IQuery<IEnumerable<UserPermissionDto>>
    {
    }
}
