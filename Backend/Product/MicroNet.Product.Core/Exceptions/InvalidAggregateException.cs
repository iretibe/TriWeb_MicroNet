namespace MicroNet.Product.Core.Exceptions
{
    public class InvalidAggregateException : DomainException
    {
        public Guid Id { get; }

        public InvalidAggregateException(Guid id) : base($"Invalid Aggregate ID {id}")
        {
            Id = id;
        }
    }
}
