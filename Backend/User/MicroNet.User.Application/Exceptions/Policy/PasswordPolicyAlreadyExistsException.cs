namespace MicroNet.User.Application.Exceptions.Policy
{
    public class PasswordPolicyAlreadyExistsException : AppException
    {
        public PasswordPolicyAlreadyExistsException(Guid code) : base($"Password policy with `{code}` already exists")
        {
        }
    }
}
