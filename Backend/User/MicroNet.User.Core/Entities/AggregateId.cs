using MicroNet.User.Core.Exceptions;

namespace MicroNet.User.Core.Entities
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public Guid Id { get; }

        public AggregateId() : this(Guid.NewGuid())
        {

        }

        public AggregateId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidAggregateException(id);
                //throw new InvalidDeviceException(id);
            }

            Id = id;
        }

        public bool Equals(AggregateId other)
        {
            if (ReferenceEquals(null, other)) return false;

            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((AggregateId)obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static implicit operator Guid(AggregateId id) => id.Id;
        public static implicit operator AggregateId(Guid id) => new AggregateId(id);

        public override string ToString() => Id.ToString();
    }
}
