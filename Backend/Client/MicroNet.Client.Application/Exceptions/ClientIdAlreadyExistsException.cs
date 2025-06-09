namespace MicroNet.Client.Application.Exceptions
{
    public class ClientIdAlreadyExistsException : AppException
    {
        public ClientIdAlreadyExistsException(Guid code) : base($"Client with Id: `{code}` already exists.")
        {
        }
    }
}
