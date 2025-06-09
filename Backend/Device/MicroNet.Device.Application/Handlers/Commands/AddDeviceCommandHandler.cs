using MicroNet.Device.Application.Commands;
using MicroNet.Device.Application.Dto;
using MicroNet.Device.Application.Events;
using MicroNet.Device.Application.Exceptions;
using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Logging;
using MicroNet.Device.Core.Repositories;
using MicroNet.Device.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using System.Text.Json;

namespace MicroNet.Device.Application.Handlers.Commands
{
    public class AddDeviceCommandHandler : ICommandHandler<AddDeviceCommand, DeviceDto>
    {
        private readonly IDeviceRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public AddDeviceCommandHandler(IDeviceRepository repository, 
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<DeviceDto> Handle(AddDeviceCommand command, CancellationToken cancellationToken)
        {
            var device = await _repository.GetDeviceByIdAsync(command.Id);
            if (device is {}) 
            { 
                throw new DeviceAlreadyExistsException(command.Id);
            }

            var newDevice = Core.Entities.Device.AddDevice(
                new DeviceCode(command.Code),
                new DeviceName(command.Name),
                new DeviceDescription(command.Description),
                new DeviceNotes(command.Notes),
                new AuditInfo(
                    command.CreatedBy,
                    DateTime.UtcNow,
                    command.UpdatedBy!,
                    command.UpdatedAt,
                    command.DeletedBy!,
                    command.DeletedAt
                )
            );

            await _repository.AddDeviceAsync(newDevice);

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new DeviceAddedEvent(newDevice.Id, newDevice.Code.Value, command.CreatedBy), "device.added");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(DeviceAddedEvent),
                Payload = JsonSerializer.Serialize(new DeviceAddedEvent(newDevice.Id, newDevice.Code.Value, command.CreatedBy)),
                AggregateId = newDevice.Id,
                AggregateType = "Device",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return new DeviceDto
            {
                Id = newDevice.Id,
                Code = newDevice.Code.Value,
                Name = newDevice.Name.Value,
                Description = newDevice.Description.Value,
                Notes = newDevice.Notes.Value,
                CreatedAt = newDevice.AuditInfo.CreatedAt,
                CreatedBy = newDevice.AuditInfo.CreatedBy,
                UpdatedAt = newDevice.AuditInfo.UpdatedAt,
                UpdatedBy = newDevice.AuditInfo.UpdatedBy,
                DeletedAt = newDevice.AuditInfo.DeletedAt,
                DeletedBy = newDevice.AuditInfo.DeletedBy,
                IsDeleted = newDevice.IsDeleted
            };
        }
    }
}
