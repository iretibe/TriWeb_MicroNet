namespace MicroNet.Device.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public virtual string Code { get; }

        protected DomainException(string code) : base(code)
        {
            Code = code;
        }
    }
}
