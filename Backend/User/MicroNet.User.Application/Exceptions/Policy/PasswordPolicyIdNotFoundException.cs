namespace MicroNet.User.Application.Exceptions.Policy
{
    public class PasswordPolicyIdNotFoundException : AppException
    {
        public PasswordPolicyIdNotFoundException(Guid id) : base($"Password Policy with ID '{id}' is not found")
        {
        }
    }
}
