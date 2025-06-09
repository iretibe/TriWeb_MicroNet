namespace MicroNet.User.Application.Exceptions.UserGroup
{
    public class UserGroupIdNotFoundException : AppException
    {
        public UserGroupIdNotFoundException(Guid code) : base($"User Group with ID: `{code}` is not found.")
        {
        }
    }
}
