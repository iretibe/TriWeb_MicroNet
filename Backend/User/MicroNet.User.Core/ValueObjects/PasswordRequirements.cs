namespace MicroNet.User.Core.ValueObjects
{
    public class PasswordRequirements : ValueObject
    {
        public int RequiredLength { get; }
        public bool RequireNonAlphanumeric { get; }
        public bool RequireDigit { get; }
        public bool RequireLowercase { get; }
        public bool RequireUppercase { get; }
        public bool RequireUniqueChars { get; }

        public PasswordRequirements(
            int requiredLength,
            bool requireNonAlphanumeric,
            bool requireDigit,
            bool requireLowercase,
            bool requireUppercase,
            bool requireUniqueChars)
        {
            RequiredLength = requiredLength;
            RequireNonAlphanumeric = requireNonAlphanumeric;
            RequireDigit = requireDigit;
            RequireLowercase = requireLowercase;
            RequireUppercase = requireUppercase;
            RequireUniqueChars = requireUniqueChars;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return RequiredLength;
            yield return RequireNonAlphanumeric;
            yield return RequireDigit;
            yield return RequireLowercase;
            yield return RequireUppercase;
            yield return RequireUniqueChars;
        }
    }
}
