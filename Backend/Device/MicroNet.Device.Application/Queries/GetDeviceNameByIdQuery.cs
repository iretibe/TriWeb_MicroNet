using MicroNet.Device.Application.Dto;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Queries
{
    public class GetDeviceNameByIdQuery : IQuery<AllDeviceNameDto>
    {
        public Guid Id { get; }

        public GetDeviceNameByIdQuery(Guid id) => Id = id;
    }
}
