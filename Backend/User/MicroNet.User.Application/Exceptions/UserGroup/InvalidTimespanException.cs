namespace MicroNet.User.Application.Exceptions.UserGroup
{
    public class InvalidTimespanException : AppException
    {
        public InvalidTimespanException(string time) : base($"Invalid StartTime format: {time}")
        {
        }
    }
}
