namespace MicroNet.Client.Core.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public AddressDto Address { get; set; } = new();
        public KYCInformationDto KYC { get; set; } = new();
    }
}
