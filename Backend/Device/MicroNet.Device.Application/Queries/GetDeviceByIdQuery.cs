using MicroNet.Device.Application.Dto;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Queries
{
    public class GetDeviceByIdQuery : IQuery<AllDeviceDto>
    {
        public Guid Id { get; }

        public GetDeviceByIdQuery(Guid id) => Id = id;
    }
}
