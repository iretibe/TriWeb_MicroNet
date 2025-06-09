namespace MicroNet.Revenue.Application.Exceptions
{
    public class RevenueReversalIdNotFoundException : AppException
    {
        public RevenueReversalIdNotFoundException(Guid code) : base($"Revenue Reversal with Id: `{code}` is not found.")
        {
        }
    }
}
