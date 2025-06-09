namespace MicroNet.Employee.Core.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string OtherName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string SocialMediaAccount { get; set; } = default!;
        public bool IsSystemUser { get; set; }
        public string EmployeeType { get; set; } = default!;
        public Guid BranchId { get; set; }
        public Guid DeviceId { get; set; }
        public Guid ZoneId { get; set; }
        public DateTime DateEmployed { get; set; }
        public byte[] Picture { get; set; } = default!;
        public string NationalId { get; set; } = default!;
        public decimal? TransactionLimit { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = new();
    }
}
