using MicroNet.Device.Application.Dto;
using MicroNet.Device.Application.Queries;
using MicroNet.Device.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Handlers.Queries
{
    public  class GetDevicesQueryHandler : IQueryHandler<GetDevicesQuery, IEnumerable<AllDeviceDto>>
    {
        private readonly IDeviceRepository _repository;

        public GetDevicesQueryHandler(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AllDeviceDto>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = await _repository.GetDevicesAsync();

            var result = devices.Select(d => new AllDeviceDto
            {
                Id = d.Id,
                Code = d.Code.Value,
                Name = d.Name.Value,
                Description = d.Description.Value,
                Notes = d.Notes.Value,
                CreatedBy = d.AuditInfo.CreatedBy,
                CreatedAt = d.AuditInfo.CreatedAt,
                UpdatedBy = d.AuditInfo.UpdatedBy,
                UpdatedAt = d.AuditInfo.UpdatedAt,
                DeletedBy = d.AuditInfo.DeletedBy,
                DeletedAt = d.AuditInfo.DeletedAt,
                IsDeleted = d.IsDeleted
            });

            return result;
        }
    }
}
