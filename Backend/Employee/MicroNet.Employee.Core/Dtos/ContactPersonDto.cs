namespace MicroNet.Employee.Core.Dtos
{
    public class ContactPersonDto
    {
        public string Name { get; set; } = default!;
        public string GhanaCard { get; set; } = default!;
        public string PrimaryPhone { get; set; } = default!;
        public string AlternatePhone { get; set; } = default!;
        public string PrimaryEmail { get; set; } = default!;
        public string AlternateEmail { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string SocialMedia { get; set; } = default!;
    }
}
