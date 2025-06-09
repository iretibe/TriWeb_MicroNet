namespace MicroNet.Sundry.Core.ValueObjects
{
    public class AuditInfo
    {
        public string? CreatedBy { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private AuditInfo() { }

        public AuditInfo(string? createdBy, DateTime? createdAt,
            string? updatedBy, DateTime? updatedAt)
        {
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }

        public static AuditInfo CreateNew(string createdBy)
            => new AuditInfo(createdBy, DateTime.UtcNow, null, null);

        public void SetUpdated(string updatedBy)
        {
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
