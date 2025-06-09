namespace MicroNet.Product.Application.Exceptions
{
    public class LoanIdNotFoundException : AppException
    {
        public LoanIdNotFoundException(Guid code) : base($"Loan with Id: `{code}` is not found.")
        {
        }
    }
}
