namespace MicroNet.Account.Application.Exceptions
{
    public class AccountIdAlreadyTerminatedException : AppException
    {
        public AccountIdAlreadyTerminatedException(string accountNumber) : base($"Account Number: `{accountNumber}` has already been terminated.")
        {
        }
    }
}
