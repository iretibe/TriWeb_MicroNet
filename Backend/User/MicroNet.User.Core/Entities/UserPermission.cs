using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Core.Entities
{
    public class UserPermission : AggregateRoot
    {
        public string UserId { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid BranchId { get; set; }
        public string RoleName { get; set; }

        public AuditInfo AuditInfo { get; set; }
        public bool IsDeleted { get; set; } = false;

        private UserPermission() { }

        public UserPermission(string userId, Guid userGroupId, Guid branchId, string roleName, AuditInfo auditInfo)
        {
            UserId = userId;
            UserGroupId = userGroupId;
            BranchId = branchId;
            RoleName = roleName;
            AuditInfo = auditInfo;
        }
    }
}
