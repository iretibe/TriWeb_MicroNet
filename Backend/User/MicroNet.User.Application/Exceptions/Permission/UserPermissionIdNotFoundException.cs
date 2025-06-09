namespace MicroNet.User.Application.Exceptions.Permission
{
    public class UserPermissionIdNotFoundException : AppException
    {
        public UserPermissionIdNotFoundException(Guid code) : base($"User Permission with ID `{code}` is not found.")
        {
        }
    }
}
