using MicroNet.Device.Application.Dto;
using MicroNet.Device.Application.Exceptions;
using MicroNet.Device.Application.Queries;
using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Handlers.Queries
{
    public class GetDeviceNameByIdQueryHandler : IQueryHandler<GetDeviceNameByIdQuery, AllDeviceNameDto>
    {
        private readonly IDeviceRepository _repository;

        public GetDeviceNameByIdQueryHandler(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllDeviceNameDto> Handle(GetDeviceNameByIdQuery request, CancellationToken cancellationToken)
        {
            var name = await _repository.GetDeviceNameByIdAsync(new AggregateId(request.Id));

            if (string.IsNullOrWhiteSpace(name))
                throw new DeviceNotFoundException(request.Id);

            return new AllDeviceNameDto
            {
                Id = request.Id,
                Name = name
            };
        }
    }
}
