namespace MicroNet.Menu.Api.Exceptions
{
    public class MenuNotFoundException : AppException
    {
        public MenuNotFoundException(string id) : base($"Menu with ID: {id} is not found!")
        {
        }
    }
}
