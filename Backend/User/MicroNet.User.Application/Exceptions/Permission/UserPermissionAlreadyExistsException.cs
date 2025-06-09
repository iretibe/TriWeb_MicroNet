namespace MicroNet.User.Application.Exceptions.Permission
{
    public class UserPermissionAlreadyExistsException : AppException
    {
        public UserPermissionAlreadyExistsException(Guid code) : base($"User Permission with ID: `{code}` already exists.")
        {
        }
    }
}
