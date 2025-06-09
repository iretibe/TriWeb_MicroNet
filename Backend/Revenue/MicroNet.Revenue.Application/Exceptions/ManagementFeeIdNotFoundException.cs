namespace MicroNet.Revenue.Application.Exceptions
{
    public class ManagementFeeIdNotFoundException : AppException
    {
        public ManagementFeeIdNotFoundException(Guid code) : base($"Management Fee with Id: `{code}` is not found.")
        {
        }
    }
}
