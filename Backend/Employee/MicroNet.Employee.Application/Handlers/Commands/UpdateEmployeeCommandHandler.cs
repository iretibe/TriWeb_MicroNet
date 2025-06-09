using MediatR;
using MicroNet.Employee.Application.Commands;
using MicroNet.Employee.Application.Exceptions;
using MicroNet.Employee.Application.Helpers;
using MicroNet.Employee.Core.Clients;
using MicroNet.Employee.Core.Dtos.External;
using MicroNet.Employee.Core.Repositories;
using MicroNet.Employee.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Employee.Application.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);

            if (existing == null)
                throw new EmployeeIdNotFoundException(request.Id);

            var dto = request.EmployeeDto;

            var contact = new ContactPerson(
                dto.ContactPerson.Name,
                dto.ContactPerson.GhanaCard,
                dto.ContactPerson.AlternatePhone,
                dto.ContactPerson.AlternatePhone,
                dto.ContactPerson.PrimaryEmail,
                dto.ContactPerson.AlternateEmail,
                dto.ContactPerson.PhysicalAddress,
                dto.ContactPerson.PostalAddress,
                dto.ContactPerson.SocialMedia
            );

            existing.Update(
                dto.FirstName, dto.Surname, dto.OtherName, dto.DateOfBirth, dto.PhoneNumber,
                dto.PhysicalAddress, dto.PostalAddress, dto.EmailAddress, dto.SocialMediaAccount,
                dto.IsSystemUser, dto.EmployeeType, dto.BranchId, dto.DeviceId, dto.ZoneId, dto.DateEmployed,
                dto.Picture, dto.NationalId, dto.TransactionLimit, contact, request.UpdatedBy
            );

            await _repository.UpdateAsync(existing);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.UpdatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Updated an Employee",
                EntityType = nameof(Core.Entities.Employee)
            });

            return Unit.Value;
        }
    }
}
