namespace MicroNet.Product.Application.Exceptions
{
    public class ProductIdAlreadyExistsException : AppException
    {
        public ProductIdAlreadyExistsException(Guid code) : base($"Product with Id: `{code}` already exists.")
        {
        }
    }
}
