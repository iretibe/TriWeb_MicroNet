namespace MicroNet.Product.Application.Exceptions
{
    public class AppException : Exception
    {
        public virtual string Code { get; }

        protected AppException(string code) : base(code)
        {
            Code = code;
        }
    }
}
