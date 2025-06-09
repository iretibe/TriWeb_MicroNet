using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Core.Entities
{
    public class UserGroupMenu : AggregateRoot
    {
        public Guid UserGroupId { get; private set; }
        public Guid MenuId { get; private set; }
        public bool IsChecked { get; private set; }
        public AuditInfo AuditInfo { get; private set; } = default!;

        private UserGroupMenu() { }

        public UserGroupMenu(Guid userGroupId, Guid menuId, bool isChecked, AuditInfo auditInfo)
        {
            UserGroupId = userGroupId;
            MenuId = menuId;
            IsChecked = isChecked;
            AuditInfo = auditInfo;
        }
    }
}
