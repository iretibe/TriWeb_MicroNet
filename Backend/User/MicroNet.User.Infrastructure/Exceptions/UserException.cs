namespace MicroNet.User.Infrastructure.Exceptions
{
    public class UserException : Exception
    {
        public virtual string Code { get; }

        protected UserException(string code) : base(code)
        {
            Code = code;
        }
    }
}
