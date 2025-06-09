using MicroNet.Device.Application.Dto;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Queries
{
    public class GetDeviceByNameQuery : IQuery<AllDeviceDto>
    {
        public string Name { get; }

        public GetDeviceByNameQuery(string name) => Name = name;
    }
}
