using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.User;

namespace MicroNet.User.Application.Queries.User
{
    public class GetAllUsersQuery : IQuery<IEnumerable<UserRecordsDto>>
    {
    }
}
