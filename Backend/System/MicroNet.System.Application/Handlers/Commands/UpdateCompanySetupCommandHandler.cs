using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.System.Application.Commands;
using MicroNet.System.Application.Exceptions;
using MicroNet.System.Application.Helpers;
using MicroNet.System.Core.Clients;
using MicroNet.System.Core.Dtos.External;
using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Enums;
using MicroNet.System.Core.Repositories;
using MicroNet.System.Core.ValueObjects;

namespace MicroNet.System.Application.Handlers.Commands
{
    public class UpdateCompanySetupCommandHandler : ICommandHandler<UpdateCompanySetupCommand>
    {
        private readonly ICompanySetupRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public UpdateCompanySetupCommandHandler(ICompanySetupRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdateCompanySetupCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);
            if (company == null) throw new CompanyIdNotFoundException(request.Id);

            var contactPerson = new ContactPerson(
                request.Dto.Contact.FirstName,
                request.Dto.Contact.LastName,
                request.Dto.Contact.Email,
                request.Dto.Contact.PhoneNumber
            );

            var integrationSettings = new IntegrationSettings(
                request.Dto.Integration.CoreBankingEnabled,
                request.Dto.Integration.TelcoIntegrationEnabled,
                request.Dto.Integration.PaymentGatewayEnabled,
                request.Dto.Integration.SftpPath,
                request.Dto.Integration.ExportFolderPath,
                request.Dto.Integration.BatchTransactionImportEnabled
            );

            var notificationSettings = request.Dto.Notification != null
                ? new NotificationSettings(
                    Enum.Parse<NotificationMode>(request.Dto.Notification.Mode), // Convert string to NotificationMode enum.
                    request.Dto.Notification.Recipients,
                    request.Dto.Notification.UseMakerChecker,
                    request.Dto.Notification.RequireTransactionLimit
                )
                : new NotificationSettings(NotificationMode.None, [], false, false);

            var auditInfo = new AuditInfo(null, null, company!.AuditInfo.UpdatedBy, DateTime.Now, null!, null);
            
            var setup = new CompanySetup(
                request.Id,
                request.Dto.CompanyName,
                request.Dto.CompanyAddress,
                request.Dto.RegistrationDate,
                contactPerson,
                request.Dto.OfficialEmail,
                request.Dto.OfficialPhoneNumber,
                request.Dto.YearOfRegistration,
                request.Dto.SSN,
                request.Dto.TIN,
                request.Dto.Prefix,
                request.Dto.Logo != null ? new CompanyLogo(request.Dto.Logo.FileName, request.Dto.Logo.Content) : null,
                integrationSettings,
                notificationSettings,
                auditInfo
            );

            await _repository.UpdateAsync(setup);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.UpdatedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(setup, SerializationHelper.Settings),
                Method = "Updated a Company Setup",
                EntityType = nameof(CompanySetup)
            });

            return Unit.Value;
        }
    }
}
