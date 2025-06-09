using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.User;

namespace MicroNet.User.Application.Queries.User
{
    public class GetUserByIdQuery : IQuery<AllUserDto>
    {
        public string Id { get; set; }

        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
