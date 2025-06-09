namespace MicroNet.User.Application.Exceptions.User
{
    public class EmailIsInUseException : AppException
    {
        public EmailIsInUseException(string email) : base($"Email {email} is already in use.")
        {
        }
    }
}
