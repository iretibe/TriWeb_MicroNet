using MicroNet.Shared.CQRS.Commands;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Application.Commands
{
    public class CreateCompanySetupCommand : ICommand<CompanySetupDto>
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = default!;
        public string CompanyAddress { get; set; } = default!;
        public DateTime RegistrationDate { get; set; }
        public string OfficialEmail { get; set; } = default!;
        public string OfficialPhoneNumber { get; set; } = default!;
        public string YearOfRegistration { get; set; } = default!;
        public string SSN { get; set; } = default!;
        public string TIN { get; set; } = default!;
        public string Prefix { get; set; } = default!;
        public ContactPersonDto Contact { get; set; } = default!;
        public IntegrationSettingsDto Integration { get; set; } = default!;
        public NotificationSettingsDto Notification { get; set; } = default!;
        public CompanyLogoDto? Logo { get; set; }
    }
}
