using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Queries.Access
{
    public class GetAllMenuListQuery : IQuery<IEnumerable<MenuEntityDto>>
    {
        public GetAllMenuListQuery()
        {
        }
    }
}
