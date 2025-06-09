using MediatR;
using MicroNet.Device.Application.Commands;
using MicroNet.Device.Application.Events;
using MicroNet.Device.Application.Exceptions;
using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Logging;
using MicroNet.Device.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using System.Text.Json;

namespace MicroNet.Device.Application.Handlers.Commands
{
    public class DeleteDeviceCommandHandler : ICommandHandler<DeleteDeviceCommand>
    {
        private readonly IDeviceRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;

        public DeleteDeviceCommandHandler(IDeviceRepository repository, 
            IMessageBroker messageBroker, IDomainEventLogger logger)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _repository.GetDeviceByIdAsync(request.Id);
            if (device is null)
            {
                throw new DeviceNotFoundException(request.Id);
            }

            device.DeleteDevice(request.Id, device.AuditInfo.DeletedBy!);
            await _repository.UpdateDeviceAsync(device);

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new DeviceDeletedEvent(device.AuditInfo.DeletedBy!, device.Id, device.AuditInfo.DeletedBy!), "device.deleted");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(DeviceDeletedEvent),
                Payload = JsonSerializer.Serialize(new DeviceDeletedEvent(device.AuditInfo.DeletedBy!, device.Id, device.AuditInfo.DeletedBy!)),
                AggregateId = device.Id,
                AggregateType = "Device",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return Unit.Value;
        }
    }
}
