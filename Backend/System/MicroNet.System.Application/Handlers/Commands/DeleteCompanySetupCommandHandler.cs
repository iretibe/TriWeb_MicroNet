using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.System.Application.Commands;
using MicroNet.System.Application.Exceptions;
using MicroNet.System.Application.Helpers;
using MicroNet.System.Core.Clients;
using MicroNet.System.Core.Dtos.External;
using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Repositories;
using MicroNet.System.Core.ValueObjects;

namespace MicroNet.System.Application.Handlers.Commands
{
    public class DeleteCompanySetupCommandHandler : ICommandHandler<DeleteCompanySetupCommand>
    {
        private readonly ICompanySetupRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteCompanySetupCommandHandler(ICompanySetupRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteCompanySetupCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);
            if (company == null)
            {
                throw new CompanyIdNotFoundException(request.Id);
            }

            var auditInfo = new AuditInfo(null, null, null, null, company!.AuditInfo.DeletedBy, DateTime.Now);

            // Delete the company setup
            await _repository.DeleteAsync(company.Id);

            // Create an audit log entry
            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.DeletedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(company, SerializationHelper.Settings),
                Method = "Deleted a Company Setup",
                EntityType = nameof(CompanySetup)
            });

            return Unit.Value;
        }
    }
}
