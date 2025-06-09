namespace MicroNet.Product.Application.Exceptions
{
    public class LoanIdAlreadyExistsException : AppException
    {
        public LoanIdAlreadyExistsException(Guid code) : base($"Loan with Id: `{code}` already exists.")
        {
        }
    }
}
