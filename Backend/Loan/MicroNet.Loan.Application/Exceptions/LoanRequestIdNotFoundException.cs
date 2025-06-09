namespace MicroNet.Loan.Application.Exceptions
{
    public class LoanRequestIdNotFoundException : AppException
    {
        public LoanRequestIdNotFoundException(Guid id) : base($"Loan Request with ID: '{id}' was not found.")
        {
        }
    }
}
