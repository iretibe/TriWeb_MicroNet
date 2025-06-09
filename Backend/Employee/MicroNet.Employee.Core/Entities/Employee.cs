using MicroNet.Employee.Core.ValueObjects;

namespace MicroNet.Employee.Core.Entities
{
    public class Employee : AggregateRoot
    {
        public string EmployeeNumber { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string OtherName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PhysicalAddress { get; private set; }
        public string PostalAddress { get; private set; }
        public string EmailAddress { get; private set; }
        public string SocialMediaAccount { get; private set; }
        public bool IsSystemUser { get; private set; }
        public string EmployeeType { get; private set; }
        public Guid BranchId { get; private set; }
        public Guid DeviceId { get; private set; }
        public Guid ZoneId { get; private set; }
        public DateTime DateEmployed { get; private set; }
        public byte[] Picture { get; private set; }
        public string NationalId { get; private set; }
        public decimal? TransactionLimit { get; private set; }
        public ContactPerson ContactPerson { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private Employee() { }

        public Employee(string employeeNumber, string firstName, string surname, string otherName, DateTime dob,
            int age, string phoneNumber, string physicalAddress, string postalAddress, string email, string socialMedia,
            bool isSystemUser, string employeeType, Guid branchId, Guid deviceId, Guid zoneId,
            DateTime dateEmployed, byte[] picture, string nationalId, decimal? transactionLimit,
            ContactPerson contactPerson, string createdBy)
        {
            Id = Guid.NewGuid();
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            Surname = surname;
            OtherName = otherName;
            DateOfBirth = dob;
            Age = age;
            PhoneNumber = phoneNumber;
            PhysicalAddress = physicalAddress;
            PostalAddress = postalAddress;
            EmailAddress = email;
            SocialMediaAccount = socialMedia;
            IsSystemUser = isSystemUser;
            EmployeeType = employeeType;
            BranchId = branchId;
            DeviceId = deviceId;
            ZoneId = zoneId;
            DateEmployed = dateEmployed;
            Picture = picture;
            NationalId = nationalId;
            TransactionLimit = transactionLimit;
            ContactPerson = contactPerson;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(string firstName, string surname, string otherName, DateTime dob, string phoneNumber,
            string physicalAddress, string postalAddress, string email, string socialMedia, bool isSystemUser,
            string employeeType, Guid branchId, Guid deviceId, Guid zoneId, DateTime dateEmployed, byte[] picture,
            string nationalId, decimal? transactionLimit, ContactPerson contactPerson, string updatedBy)
        {
            FirstName = firstName;
            Surname = surname;
            OtherName = otherName;
            DateOfBirth = dob;
            PhoneNumber = phoneNumber;
            PhysicalAddress = physicalAddress;
            PostalAddress = postalAddress;
            EmailAddress = email;
            SocialMediaAccount = socialMedia;
            IsSystemUser = isSystemUser;
            EmployeeType = employeeType;
            BranchId = branchId;
            DeviceId = deviceId;
            ZoneId = zoneId;
            DateEmployed = dateEmployed;
            Picture = picture;
            NationalId = nationalId;
            TransactionLimit = transactionLimit;
            ContactPerson = contactPerson;
            AuditInfo.SetUpdated(AuditInfo.UpdatedBy!);
        }
    }
}