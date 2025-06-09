namespace MicroNet.Account.Core.Exceptions
{
    public class DomainException : Exception
    {
        public virtual string Code { get; }

        protected DomainException(string code) : base(code)
        {
            Code = code;
        }
    }
}
