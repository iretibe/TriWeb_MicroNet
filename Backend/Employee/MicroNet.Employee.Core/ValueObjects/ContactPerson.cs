namespace MicroNet.Employee.Core.ValueObjects
{
    public class ContactPerson
    {
        public string Name { get; private set; }
        public string GhanaCard { get; private set; }
        public string PrimaryPhone { get; private set; }
        public string AlternatePhone { get; private set; }
        public string PrimaryEmail { get; private set; }
        public string AlternateEmail { get; private set; }
        public string PhysicalAddress { get; private set; }
        public string PostalAddress { get; private set; }
        public string SocialMedia { get; private set; }

        private ContactPerson() { }

        public ContactPerson(string name, string ghanaCard, string primaryPhone, string alternatePhone,
            string primaryEmail, string alternateEmail, string physicalAddress, string postalAddress, string socialMedia)
        {
            Name = name;
            GhanaCard = ghanaCard;
            PrimaryPhone = primaryPhone;
            AlternatePhone = alternatePhone;
            PrimaryEmail = primaryEmail;
            AlternateEmail = alternateEmail;
            PhysicalAddress = physicalAddress;
            PostalAddress = postalAddress;
            SocialMedia = socialMedia;
        }
    }
}
