namespace MicroNet.Pos.Core.ValueObjects
{
    public class AuditInfo
    {
        public string CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private AuditInfo() { }

        public AuditInfo(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        public static AuditInfo CreateNew(string createdBy) => new AuditInfo(createdBy);

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
