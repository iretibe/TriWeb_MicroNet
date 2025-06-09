namespace MicroNet.Client.Application.Exceptions
{
    public class ClientNameAlreadyExistsException : AppException
    {
        public ClientNameAlreadyExistsException(string code) : base($"Client with name: `{code}` already exists.")
        {
        }
    }
}
