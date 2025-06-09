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
    public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public AddEmployeeCommandHandler(IEmployeeRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Guid> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.EmployeeDto;

            var record = await _repository.GetByIdAsync(request.EmployeeDto.Id);
            if (record is not null)
            {
                throw new EmployeeIdAlreadyExistsException(request.EmployeeDto.Id);
            }

            var contact = new ContactPerson(
                dto.ContactPerson.Name,
                dto.ContactPerson.GhanaCard,
                dto.ContactPerson.PrimaryPhone,
                dto.ContactPerson.AlternatePhone,
                dto.ContactPerson.PrimaryEmail,
                dto.ContactPerson.AlternateEmail,
                dto.ContactPerson.PhysicalAddress,
                dto.ContactPerson.PostalAddress,
                dto.ContactPerson.SocialMedia
            );

            var auditInfo = new AuditInfo(record!.AuditInfo.CreatedBy, DateTime.Now, null!, null, null!, null);

            int age = (int)((DateTime.Now - dto.DateOfBirth).TotalDays / 365.25);

            var employee = new Core.Entities.Employee(
                dto.EmployeeNumber,
                dto.FirstName,
                dto.Surname,
                dto.OtherName,
                dto.DateOfBirth,
                age,
                dto.PhoneNumber,
                dto.PhysicalAddress,
                dto.PostalAddress,
                dto.EmailAddress,
                dto.SocialMediaAccount,
                dto.IsSystemUser,
                dto.EmployeeType,
                dto.BranchId,
                dto.DeviceId,
                dto.ZoneId,
                dto.DateEmployed,
                dto.Picture,
                dto.NationalId,
                dto.TransactionLimit,
                contact,
                request.CreatedBy
            );

            await _repository.AddAsync(employee);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.CreatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Created an Employee",
                EntityType = nameof(Core.Entities.Employee)
            });

            return employee.Id;
        }
    }
}
