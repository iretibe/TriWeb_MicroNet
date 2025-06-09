using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Queries.Access
{
    public class GetAllAssignedSystemMenusByUserIdQuery : IQuery<AssignedMenusDto1>
    {
        public string UserId { get; set; }

        public GetAllAssignedSystemMenusByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
