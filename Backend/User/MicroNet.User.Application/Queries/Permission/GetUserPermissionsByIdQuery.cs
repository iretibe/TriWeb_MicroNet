using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Application.Queries.Permission
{
    public class GetUserPermissionsByIdQuery : IQuery<UserPermission>
    {
        public Guid Id { get; set; }

        public GetUserPermissionsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
