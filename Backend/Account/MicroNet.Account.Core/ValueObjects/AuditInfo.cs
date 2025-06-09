namespace MicroNet.Account.Core.ValueObjects
{
    public class AuditInfo
    {
        public string CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string DeletedBy { get; private set; }
        public DateTime DeletedAt { get; private set; }
        public string? ApprovedBy { get; private set; }
        public DateTime? ApprovedAt { get; private set; }

        private AuditInfo() { }

        public AuditInfo(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        public void Approve(string approver)
        {
            ApprovedBy = approver;
            ApprovedAt = DateTime.UtcNow;
        }

        public void SetUpdated(string updatedBy)
        {
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeleted(string deletedBy)
        {
            DeletedBy = deletedBy;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
