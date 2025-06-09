namespace MicroNet.User.Application.Exceptions.UserGroup
{
    public class UserGroupAlreadyExistsException : AppException
    {
        public UserGroupAlreadyExistsException(Guid code) : base($"User Group with ID: `{code}` already exists.")
        {
        }
    }
}
