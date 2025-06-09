namespace MicroNet.Client.Application.Exceptions
{
    public class ClientIdNotFoundException : AppException
    {
        public ClientIdNotFoundException(Guid code) : base($"Client with Id: `{code}` is not found.")
        {
        }
    }
}
