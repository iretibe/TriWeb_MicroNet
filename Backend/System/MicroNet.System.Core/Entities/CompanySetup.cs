using MicroNet.System.Core.ValueObjects;

namespace MicroNet.System.Core.Entities
{
    public class CompanySetup : AggregateRoot
    {
        public string CompanyName { get; private set; }
        public string CompanyAddress { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public ContactPerson ContactPerson { get; private set; }
        public string OfficialEmail { get; private set; }
        public string OfficialPhoneNumber { get; private set; }
        public string YearOfRegistration { get; private set; }
        public string SSN { get; private set; }
        public string TIN { get; private set; }
        public string Prefix { get; private set; }
        public CompanyLogo? Logo { get; private set; }

        public AuditInfo AuditInfo { get; private set; } = default!;

        public IntegrationSettings IntegrationSettings { get; private set; }
        public NotificationSettings NotificationSettings { get; private set; }

        private CompanySetup() { } // EF

        public CompanySetup(Guid id, string companyName, string companyAddress,
            DateTime registrationDate, ContactPerson contactPerson, string officialEmail,
            string officialPhoneNumber, string yearOfRegistration, string ssn, string tin,
            string prefix, CompanyLogo? logo, IntegrationSettings integrationSettings,
            NotificationSettings notificationSettings, AuditInfo auditInfo)
        {
            Id = id;
            CompanyName = companyName;
            CompanyAddress = companyAddress;
            RegistrationDate = registrationDate;
            ContactPerson = contactPerson;
            OfficialEmail = officialEmail;
            OfficialPhoneNumber = officialPhoneNumber;
            YearOfRegistration = yearOfRegistration;
            SSN = ssn;
            TIN = tin;
            Prefix = prefix;
            Logo = logo;
            IntegrationSettings = integrationSettings;
            NotificationSettings = notificationSettings;
            AuditInfo = auditInfo;
        }

        public void UpdateDetails(CompanySetup updated, string updatedBy)
        {
            CompanyName = updated.CompanyName;
            CompanyAddress = updated.CompanyAddress;
            RegistrationDate = updated.RegistrationDate;
            OfficialEmail = updated.OfficialEmail;
            OfficialPhoneNumber = updated.OfficialPhoneNumber;
            YearOfRegistration = updated.YearOfRegistration;
            SSN = updated.SSN;
            TIN = updated.TIN;
            Prefix = updated.Prefix;
            Logo = updated.Logo;
            IntegrationSettings = updated.IntegrationSettings;
            NotificationSettings = updated.NotificationSettings;

            AuditInfo.SetUpdated(updatedBy);
        }

        public void UpdateLogo(CompanyLogo? newLogo)
        {
            Logo = newLogo;
        }

        public void UpdateIntegrationSettings(IntegrationSettings settings)
        {
            IntegrationSettings = settings;
        }

        public void UpdateNotificationSettings(NotificationSettings settings)
        {
            NotificationSettings = settings;
        }
    }
}
