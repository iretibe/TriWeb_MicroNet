using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Core.Entities
{
    public class PasswordPolicy : AggregateRoot
    {
        public string PolicyName { get; private set; }
        public PasswordRequirements Requirements { get; private set; }
        public AuditInfo AuditInfo { get; private set; } = default!;
        public Guid UserGroupId { get; private set; }
        public bool IsDeleted { get; private set; }

        private PasswordPolicy() { }

        public PasswordPolicy(string policyName, PasswordRequirements requirements, Guid userGroupId, AuditInfo auditInfo)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
            Requirements = requirements;
            UserGroupId = userGroupId;
            AuditInfo = auditInfo;
            IsDeleted = false;
        }

        public void UpdatePolicy(Guid id, PasswordRequirements requirements, string updatedBy)
        {
            Id = id;
            Requirements = requirements;
            AuditInfo = new AuditInfo(
                AuditInfo.CreatedBy,
                AuditInfo.CreatedAt,
                updatedBy,
                DateTime.UtcNow,
                AuditInfo.DeletedBy!,
                AuditInfo.DeletedAt
            );
        }

        public void MarkDeleted() => IsDeleted = true;
    }
}
