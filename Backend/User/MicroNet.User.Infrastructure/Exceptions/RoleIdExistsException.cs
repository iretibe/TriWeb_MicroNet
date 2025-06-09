namespace MicroNet.User.Infrastructure.Exceptions
{
    public class RoleIdExistsException : UserException
    {
        public RoleIdExistsException(string id) : base($"Role with ID `{id}` already exists")
        {
        }
    }
}
