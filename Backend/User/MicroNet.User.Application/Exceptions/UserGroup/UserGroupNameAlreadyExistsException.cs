namespace MicroNet.User.Application.Exceptions.UserGroup
{
    public class UserGroupNameAlreadyExistsException : AppException
    {
        public UserGroupNameAlreadyExistsException(string code) : base($"User Group with Name: `{code}` already exists.")
        {
        }
    }
}
