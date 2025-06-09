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
    public class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand>
    {
        private readonly IClientRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public UpdateClientCommandHandler(IClientRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetByIdAsync(request.Id);
            if (client == null) throw new ClientIdNotFoundException(request.Id);

            var dto = request.ClientDto;
            var address = new Address(dto.Address.Street, dto.Address.City, dto.Address.State, dto.Address.ZipCode);
            client.Update(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber, dto.DateOfBirth, address, request.UpdatedBy);

            await _repository.UpdateAsync(client);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.UpdatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Updated a Company Client",
                EntityType = nameof(Core.Entities.Client)
            });

            return Unit.Value;
        }
    }
}
