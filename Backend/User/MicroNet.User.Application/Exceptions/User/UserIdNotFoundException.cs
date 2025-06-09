namespace MicroNet.User.Application.Exceptions.User
{
    public class UserIdNotFoundException : AppException
    {
        public UserIdNotFoundException(string id) : base($"User with ID: {id} is not found!")
        {
        }
    }
}
