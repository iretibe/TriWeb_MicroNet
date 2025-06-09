using MediatR;
using MicroNet.Client.Application.Commands;
using MicroNet.Client.Application.Exceptions;
using MicroNet.Client.Application.Helpers;
using MicroNet.Client.Core.Clients;
using MicroNet.Client.Core.Dtos.External;
using MicroNet.Client.Core.Repositories;
using MicroNet.Client.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Client.Application.Handlers.Commands
{
    public class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
    {
        private readonly IClientRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteClientCommandHandler(IClientRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.Id);
            if (client == null) throw new ClientIdNotFoundException(request.Id);

            var auditInfo = new AuditInfo(null, null, null, null, client!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(client);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.DeletedBy!, // Assuming CreatedBy is part of the command
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(client, SerializationHelper.Settings),
                Method = "Deleted a Company Client",
                EntityType = nameof(Core.Entities.Client)
            });

            return Unit.Value;
        }
    }
}
