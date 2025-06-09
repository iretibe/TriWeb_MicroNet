namespace MicroNet.Account.Application.Exceptions
{
    public class InvalidWithdrawalAmountException : AppException
    {
        public InvalidWithdrawalAmountException(decimal amount) : base($"Withdrawal amount: `{amount}` should be greater than `{0}`")
        {
        }
    }
}
