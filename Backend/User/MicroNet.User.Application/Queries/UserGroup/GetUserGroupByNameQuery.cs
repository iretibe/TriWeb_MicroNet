using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.User.Application.Queries.UserGroup
{
    public class GetUserGroupByNameQuery : IQuery<Core.Entities.UserGroup>
    {
        public string UserGroupName { get; set; }
        public Guid BranchId { get; set; }

        public GetUserGroupByNameQuery(string userGroupName, Guid branchId)
        {
            UserGroupName = userGroupName;
            BranchId = branchId;
        }
    }
}
