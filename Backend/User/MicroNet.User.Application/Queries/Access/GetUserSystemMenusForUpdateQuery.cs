using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Queries.Access
{
    public class GetUserSystemMenusForUpdateQuery : IQuery<AssignedMenusForUpdateDto>
    {
        public string UserId { get; set; }

        public GetUserSystemMenusForUpdateQuery(string userId)
        {
            UserId = userId;
        }
    }
}
