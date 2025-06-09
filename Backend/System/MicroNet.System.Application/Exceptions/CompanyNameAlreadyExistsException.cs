namespace MicroNet.System.Application.Exceptions
{
    public class CompanyNameAlreadyExistsException : AppException
    {
        public CompanyNameAlreadyExistsException(string code) : base($"Company with name: `{code}` already exists.")
        {
        }
    }
}
