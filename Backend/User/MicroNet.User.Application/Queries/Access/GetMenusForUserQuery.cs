using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Queries.Access
{
    public class GetMenusForUserQuery : IQuery<List<MenuEntityDto>>
    {
        public string UserId { get; set; }

        public GetMenusForUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
