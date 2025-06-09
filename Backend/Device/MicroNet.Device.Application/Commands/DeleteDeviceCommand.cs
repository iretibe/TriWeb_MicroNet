using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Device.Application.Commands
{
    public class DeleteDeviceCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteDeviceCommand(Guid id)
        {
            Id = id;
        }
    }
}
