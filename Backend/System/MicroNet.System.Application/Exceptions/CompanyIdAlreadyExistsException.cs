namespace MicroNet.System.Application.Exceptions
{
    internal class CompanyIdAlreadyExistsException : AppException
    {
        public CompanyIdAlreadyExistsException(Guid code) : base($"Company with Id: `{code}` already exists.")
        {
        }
    }
}
