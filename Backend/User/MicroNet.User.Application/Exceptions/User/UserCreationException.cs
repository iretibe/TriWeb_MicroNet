namespace MicroNet.User.Application.Exceptions.User
{
    public class UserCreationException : AppException
    {
        public UserCreationException(string message) : base($"An error {message} occurred.")
        {
        }
    }
}
