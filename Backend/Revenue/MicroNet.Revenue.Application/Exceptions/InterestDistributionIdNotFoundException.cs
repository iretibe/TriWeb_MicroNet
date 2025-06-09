namespace MicroNet.Revenue.Application.Exceptions
{
    public class InterestDistributionIdNotFoundException : AppException
    {
        public InterestDistributionIdNotFoundException(Guid code) : base($"Interest Distribution with Id: `{code}` is not found.")
        {
        }
    }
}
