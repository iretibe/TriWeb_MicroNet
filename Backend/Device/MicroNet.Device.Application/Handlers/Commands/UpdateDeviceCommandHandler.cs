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
    public class UpdateDeviceCommandHandler : ICommandHandler<UpdateDeviceCommand, DeviceDto>
    {
        private readonly IDeviceRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public UpdateDeviceCommandHandler(IDeviceRepository repository, 
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<DeviceDto> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _repository.GetDeviceByIdAsync(request.Id);
            if (device is null)
            {
                throw new DeviceNotFoundException(request.Id);
            }

            // Update the device
            device.UpdateDevice(
                request.Id,
                new DeviceCode(request.Code),
                new DeviceName(request.Name),
                new DeviceDescription(request.Description),
                new DeviceNotes(request.Notes),
                request.CreatedBy,
                request.UpdatedBy!
            );

            await _repository.UpdateDeviceAsync(device);

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new DeviceUpdatedEvent(device.AuditInfo.UpdatedBy!, device.Id, device.Code.Value, device.Name.Value, device.AuditInfo.DeletedBy!, (DateTime)device.AuditInfo.UpdatedAt!), "device.updated");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(DeviceUpdatedEvent),
                Payload = JsonSerializer.Serialize(new DeviceUpdatedEvent(device.AuditInfo.UpdatedBy!, device.Id, device.Code.Value, device.Name.Value, device.AuditInfo.DeletedBy!, (DateTime)device.AuditInfo.UpdatedAt!)),
                AggregateId = device.Id,
                AggregateType = "Device",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            // Return updated DTO
            return new DeviceDto
            {
                Id = device.Id,
                Code = device.Code.Value,
                Name = device.Name.Value,
                Description = device.Description.Value,
                Notes = device.Notes.Value,
                CreatedBy = device.AuditInfo.CreatedBy,
                CreatedAt = device.AuditInfo.CreatedAt,
                UpdatedBy = device.AuditInfo.UpdatedBy,
                UpdatedAt = DateTime.Now,
                DeletedBy = device.AuditInfo.DeletedBy,
                DeletedAt = device.AuditInfo.DeletedAt,
                IsDeleted = device.IsDeleted
            };
        }
    }
}
