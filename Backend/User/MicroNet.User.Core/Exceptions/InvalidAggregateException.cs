namespace MicroNet.User.Core.Exceptions
{
    public class InvalidAggregateException : UserDomainException
    {
        public Guid Id { get; }

        public InvalidAggregateException(Guid id) : base($"Invalid Aggregate ID {id}")
        {
            Id = id;
        }
    }
}
