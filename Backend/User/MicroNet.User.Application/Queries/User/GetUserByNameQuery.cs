using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.User;

namespace MicroNet.User.Application.Queries.User
{
    public class GetUserByNameQuery : IQuery<AllUserDto>
    {
        public string UserName { get; set; }

        public GetUserByNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
