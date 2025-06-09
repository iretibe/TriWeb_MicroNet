namespace MicroNet.User.Application.Exceptions.User
{
    public class UserByNameNotFoundException : AppException
    {
        public UserByNameNotFoundException(string name) : base($"User with User name: '{name}' is not found!")
        {
        }
    }
}
