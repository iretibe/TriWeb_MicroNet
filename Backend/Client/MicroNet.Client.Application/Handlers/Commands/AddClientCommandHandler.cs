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
    public class AddClientCommandHandler : ICommandHandler<AddClientCommand, Guid>
    {
        private readonly IClientRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public AddClientCommandHandler(IClientRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Guid> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var clientDto = request.ClientDto;

            var record = await _repository.GetByIdAsync(request.ClientDto.Id);
            if (record is not null)
            {
                throw new ClientIdAlreadyExistsException(request.ClientDto.Id);
            }

            var auditInfo = new AuditInfo(record!.AuditInfo.CreatedBy, DateTime.Now, null!, null, null!, null);
            var address = new Address(clientDto.Address.Street, clientDto.Address.City, clientDto.Address.State, clientDto.Address.ZipCode);
            var kyc = new KYCInformation(clientDto.KYC.DocumentType, clientDto.KYC.DocumentNumber, clientDto.KYC.ExpiryDate);
            var client = new Core.Entities.Client(clientDto.FirstName, clientDto.LastName, clientDto.Email, clientDto.PhoneNumber, clientDto.DateOfBirth, address, kyc, request.CreatedBy);

            await _repository.AddAsync(client);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.CreatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(clientDto, SerializationHelper.Settings),
                Method = "Created a Company Client",
                EntityType = nameof(Core.Entities.Client)
            });

            return client.Id;
        }
    }
}
