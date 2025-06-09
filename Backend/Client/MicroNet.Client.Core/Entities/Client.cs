using MicroNet.Client.Core.ValueObjects;

namespace MicroNet.Client.Core.Entities
{
    public class Client : AggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Address Address { get; private set; }
        public KYCInformation KYC { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private Client() { }

        public Client(string firstName, string lastName, string email, string phoneNumber,
                      DateTime dob, Address address, KYCInformation kyc, string createdBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dob;
            Address = address;
            KYC = kyc;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(string firstName, string lastName, string email, string phoneNumber,
                           DateTime dob, Address address, string updatedBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dob;
            Address = address;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}
