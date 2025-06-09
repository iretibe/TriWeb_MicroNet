namespace MicroNet.User.Core.Exceptions
{
    public class UserDomainException : Exception
    {
        public virtual string Code { get; }

        protected UserDomainException(string message) : base(message)
        {
        }
    }
}
