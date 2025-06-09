namespace MicroNet.System.Application.Exceptions
{
    public class CompanyIdNotFoundException : AppException
    {
        public CompanyIdNotFoundException(Guid code) : base($"Company with Id: `{code}` is not found.")
        {
        }
    }
}
