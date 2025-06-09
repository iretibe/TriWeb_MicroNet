namespace MicroNet.Pos.Application.Exceptions
{
    public class TransactionIdNotFoundException : AppException
    {
        public TransactionIdNotFoundException(Guid id) : base($"Transaction with ID: `{id}` was not found.")
        {
        }
    }
}
