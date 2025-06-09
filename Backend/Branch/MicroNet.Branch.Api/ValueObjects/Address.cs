namespace MicroNet.Branch.Api.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string? PostalAddress { get; private set; }

        private Address() { }

        public Address(string street, string? physicalAddress)
        {
            Street = street;
            PostalAddress = physicalAddress;
        }
    }
}
