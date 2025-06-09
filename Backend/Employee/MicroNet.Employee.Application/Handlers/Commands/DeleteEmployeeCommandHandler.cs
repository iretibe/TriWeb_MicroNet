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
    public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.Id);
            if (employee == null) throw new EmployeeIdNotFoundException(request.Id);

            var auditInfo = new AuditInfo(null, null, null, null, employee!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(employee);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.DeletedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(employee, SerializationHelper.Settings),
                Method = "Deleted an Employee",
                EntityType = nameof(Core.Entities.Employee)
            });

            return Unit.Value;
        }
    }
}
