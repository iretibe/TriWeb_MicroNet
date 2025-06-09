namespace MicroNet.Product.Application.Exceptions
{
    public class ProductIdNotFoundException : AppException
    {
        public ProductIdNotFoundException(Guid code) : base($"Product with Id: `{code}` is not found.")
        {
        }
    }
}
