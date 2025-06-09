using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.ValueObjects
{
    [Owned]
    public sealed class AuditInfo : ValueObject
    {
        public string CreatedBy { get; } = default!;
        public DateTime? CreatedAt { get; } = default!;
        public string? UpdatedBy { get; }
        public DateTime? UpdatedAt { get; }
        public string? DeletedBy { get; }
        public DateTime? DeletedAt { get; }

        // Required by EF Core
        private AuditInfo() { }

        public AuditInfo(string createdBy, DateTime? createdAt,
            string updatedBy, DateTime? updateAt, string deletedBy, DateTime? deletedAt)
        {
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            UpdatedBy = updatedBy;
            UpdatedAt = updateAt;
            DeletedBy = deletedBy;
            DeletedAt = deletedAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CreatedBy;
            yield return CreatedAt;
            yield return UpdatedBy ?? string.Empty;
            yield return UpdatedAt ?? DateTime.MinValue;
            yield return DeletedBy ?? string.Empty;
            yield return DeletedAt ?? DateTime.MinValue;
        }
    }
}
