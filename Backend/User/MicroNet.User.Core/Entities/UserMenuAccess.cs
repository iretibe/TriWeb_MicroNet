using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Core.Entities
{
    public class UserMenuAccess : AggregateRoot
    {
        public string UserId { get; private set; }
        public Guid MenuId { get; private set; }
        public bool IsChecked { get; private set; }
        //public virtual Menu Menu { get; private set; }
        public AuditInfo AuditInfo { get; private set; } = default!;

        public UserMenuAccess() { }

        public UserMenuAccess(string userId, Guid menuId, bool isChecked)
        {
            UserId = userId;
            MenuId = menuId;
            IsChecked = isChecked;
        }

        public void SetAuditInfo(string createdBy)
        {
            AuditInfo = new AuditInfo(
                createdBy,
                DateTime.UtcNow,
                null!,
                null,
                null!,
                null
            );
        }

        public void UpdateAccess(bool isChecked, string updatedBy)
        {
            IsChecked = isChecked;
            AuditInfo = new AuditInfo(
                AuditInfo.CreatedBy,
                AuditInfo.CreatedAt,
                updatedBy,
                DateTime.UtcNow,
                AuditInfo.DeletedBy!,
                AuditInfo.DeletedAt
            );
        }
    }
}
