namespace MicroNet.User.Core.Entities
{
    public class UserSubGroupMenu : AggregateRoot
    {
        public Guid UserGroupId { get; private set; }
        public Guid MenuId { get; private set; }
        public Guid SubMenuId { get; private set; }
        public bool IsChecked { get; private set; }

        private UserSubGroupMenu() { }

        public UserSubGroupMenu(Guid userGroupId, Guid menuId, Guid subMenuId, bool isChecked)
        {
            UserGroupId = userGroupId;
            MenuId = menuId;
            SubMenuId = subMenuId;
            IsChecked = isChecked;
        }
    }
}
