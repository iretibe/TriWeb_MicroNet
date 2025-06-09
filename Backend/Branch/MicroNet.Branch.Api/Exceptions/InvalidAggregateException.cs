namespace MicroNet.Branch.Api.Exceptions
{
    public class InvalidAggregateException : AppException
    {
        public Guid Id { get; }

        public InvalidAggregateException(Guid id) : base($"Invalid Aggregate ID {id}")
        {
            Id = id;
        }
    }
}
