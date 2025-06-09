using MicroNet.Shared.CQRS.Commands;
using MicroNet.System.Application.Commands;
using MicroNet.System.Application.Exceptions;
using MicroNet.System.Application.Helpers;
using MicroNet.System.Core.Clients;
using MicroNet.System.Core.Dtos;
using MicroNet.System.Core.Dtos.External;
using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Enums;
using MicroNet.System.Core.Repositories;
using MicroNet.System.Core.ValueObjects;

namespace MicroNet.System.Application.Handlers.Commands
{
    public class CreateCompanySetupCommandHandler : ICommandHandler<CreateCompanySetupCommand, CompanySetupDto>
    {
        private readonly ICompanySetupRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public CreateCompanySetupCommandHandler(ICompanySetupRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<CompanySetupDto> Handle(CreateCompanySetupCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);
            if (company is not null)
            {
                throw new CompanyIdAlreadyExistsException(request.Id);
            }

            if (await _repository.ExistsAsync(request.CompanyName))
                throw new CompanyNameAlreadyExistsException("Company setup already exists.");

            var contactPerson = new ContactPerson(
                request.Contact.FirstName,
                request.Contact.LastName,
                request.Contact.Email,
                request.Contact.PhoneNumber
            );

            var integrationSettings = new IntegrationSettings(
                request.Integration.CoreBankingEnabled,
                request.Integration.TelcoIntegrationEnabled,
                request.Integration.PaymentGatewayEnabled,
                request.Integration.SftpPath,
                request.Integration.ExportFolderPath,
                request.Integration.BatchTransactionImportEnabled
            );

            var notificationSettings = request.Notification != null
                ? new NotificationSettings(
                    Enum.Parse<NotificationMode>(request.Notification.Mode), // Convert string to NotificationMode enum.
                    request.Notification.Recipients,
                    request.Notification.UseMakerChecker,
                    request.Notification.RequireTransactionLimit
                )
                : new NotificationSettings(NotificationMode.None, [], false, false);

            var auditInfo = new AuditInfo(company!.AuditInfo.CreatedBy, DateTime.Now, null!, null, null!, null);
            
            var setup = new CompanySetup(
                Guid.NewGuid(),
                request.CompanyName,
                request.CompanyAddress,
                request.RegistrationDate,
                contactPerson,
                request.OfficialEmail,
                request.OfficialPhoneNumber,
                request.YearOfRegistration,
                request.SSN,
                request.TIN,
                request.Prefix,
                request.Logo != null ? new CompanyLogo(request.Logo.FileName, request.Logo.Content) : null,
                integrationSettings,
                notificationSettings,
                auditInfo
            );

            await _repository.AddAsync(setup);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.CreatedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(setup, SerializationHelper.Settings),
                Method = "Created a Company Setup",
                EntityType = nameof(CompanySetup)
            });

            return new CompanySetupDto
            {
                Id = setup.Id,
                CompanyName = setup.CompanyName,
                CompanyAddress = setup.CompanyAddress,
                RegistrationDate = setup.RegistrationDate,
                OfficialEmail = setup.OfficialEmail,
                OfficialPhoneNumber = setup.OfficialPhoneNumber,
                YearOfRegistration = setup.YearOfRegistration,
                SSN = setup.SSN,
                TIN = setup.TIN,
                Prefix = setup.Prefix,
                Contact = new ContactPersonDto
                {
                    FirstName = setup.ContactPerson.FirstName,
                    LastName = setup.ContactPerson.LastName,
                    Email = setup.ContactPerson.Email,
                    PhoneNumber = setup.ContactPerson.PhoneNumber
                },
                Integration = new IntegrationSettingsDto
                {
                    CoreBankingEnabled = setup.IntegrationSettings.CoreBankingEnabled,
                    TelcoIntegrationEnabled = setup.IntegrationSettings.TelcoIntegrationEnabled,
                    PaymentGatewayEnabled = setup.IntegrationSettings.PaymentGatewayEnabled,
                    SftpPath = setup.IntegrationSettings.SftpPath!,
                    ExportFolderPath = setup.IntegrationSettings.ExportFolderPath!,
                    BatchTransactionImportEnabled = setup.IntegrationSettings.BatchTransactionImportEnabled
                },
                Notification = new NotificationSettingsDto
                {
                    Mode = setup.NotificationSettings.Mode.ToString(),
                    Recipients = setup.NotificationSettings.Recipients.ToList(),
                    UseMakerChecker = setup.NotificationSettings.UseMakerChecker,
                    RequireTransactionLimit = setup.NotificationSettings.RequireTransactionLimit
                },
                Logo = setup.Logo != null ? new CompanyLogoDto { FileName = setup.Logo.FileName, Content = setup.Logo.Content } : null
            };
        }
    }
}
