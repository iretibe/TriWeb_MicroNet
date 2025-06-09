using MicroNet.Device.Application.Dto;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Device.Application.Queries
{
    public class GetDevicesQuery : IQuery<IEnumerable<AllDeviceDto>>
    {
    }
}
