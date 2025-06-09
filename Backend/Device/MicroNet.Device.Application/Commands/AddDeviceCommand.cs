using MicroNet.Device.Application.Dto;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Device.Application.Commands
{
    public class AddDeviceCommand : ICommand<DeviceDto>
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; } = false;
    }
}
