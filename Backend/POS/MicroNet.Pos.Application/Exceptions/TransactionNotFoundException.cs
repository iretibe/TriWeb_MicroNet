namespace MicroNet.Pos.Application.Exceptions
{
    public class TransactionNotFoundException : AppException
    {
        public TransactionNotFoundException() : base($"No transactions were found.")
        {
        }
    }
}
