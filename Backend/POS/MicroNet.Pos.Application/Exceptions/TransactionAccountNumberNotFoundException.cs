namespace MicroNet.Pos.Application.Exceptions
{
    public class TransactionAccountNumberNotFoundException : AppException
    {
        public TransactionAccountNumberNotFoundException(string accountNumber) : base($"Transaction with Account Number: `{accountNumber}` was not found.")
        {
        }
    }
}
