namespace MicroNet.Revenue.Application.Exceptions
{
    public class TransactionIdNotFoundException : AppException
    {
        public TransactionIdNotFoundException(Guid code) : base($"Transaction with Id: `{code}` is not found.")
        {
        }
    }
}
