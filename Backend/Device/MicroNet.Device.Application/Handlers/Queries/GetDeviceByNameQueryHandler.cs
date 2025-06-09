using MicroNet.Device.Application.Dto;
using MicroNet.Device.Application.Exceptions;
using MicroNet.Device.Application.Queries;
using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Handlers.Queries
{
    public class GetDeviceByNameQueryHandler : IQueryHandler<GetDeviceByNameQuery, AllDeviceDto>
    {
        private readonly IDeviceRepository _repository;

        public GetDeviceByNameQueryHandler(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllDeviceDto> Handle(GetDeviceByNameQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.GetDeviceByNameAsync(request.Name);

            if (string.IsNullOrWhiteSpace(query))
                throw new DeviceNameNotFoundException(request.Name);

            var deviceId = Guid.Parse(query);
            var device = await _repository.GetDeviceByIdAsync(new AggregateId(deviceId));

            return new AllDeviceDto
            {
                Id = device.Id,
                Code = device.Code.Value,
                Name = device.Name.Value,
                Description = device.Description.Value,
                Notes = device.Notes.Value,
                CreatedBy = device.AuditInfo.CreatedBy,
                CreatedAt = device.AuditInfo.CreatedAt,
                UpdatedBy = device.AuditInfo.UpdatedBy,
                UpdatedAt = device.AuditInfo.UpdatedAt,
                DeletedBy = device.AuditInfo.DeletedBy,
                DeletedAt = device.AuditInfo.DeletedAt,
                IsDeleted = device.IsDeleted
            };
        }
    }
}
