using MicroNet.Device.Core.Entities;
using MicroNet.Device.Core.Repositories;
using MicroNet.Device.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Device.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceContext _context;

        public DeviceRepository(DeviceContext context)
        {
            _context = context;
        }

        public async Task AddDeviceAsync(Core.Entities.Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(AggregateId deviceId)
        {
            var query = await _context.Devices.Where(x => x.Id == deviceId).FirstOrDefaultAsync();
            if (query != null)
            {
                _context.Devices.Remove(query);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Core.Entities.Device> GetDeviceByIdAsync(AggregateId deviceId)
        {
            var query = await _context.Devices.Where(x => x.Id == deviceId && x.IsDeleted != true).FirstOrDefaultAsync();

            return query!;
        }

        public async Task<string> GetDeviceByNameAsync(string deviceName)
        {
            var query = await _context.Devices.Where(x => x.Name.Value == deviceName && x.IsDeleted != true).FirstOrDefaultAsync();

            return query is not null ? query.Id.ToString() : string.Empty;
        }

        public async Task<string> GetDeviceNameByIdAsync(AggregateId deviceId)
        {
            var query = await _context.Devices.Where(x => x.Id == deviceId && x.IsDeleted != true).FirstOrDefaultAsync();

            return query!.Name.Value;
        }

        public async Task<IEnumerable<Core.Entities.Device>> GetDevicesAsync()
        {
            var query = await _context.Devices.Where(x => x.IsDeleted != true).ToListAsync();

            return query;
        }

        public async Task UpdateDeviceAsync(Core.Entities.Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }
    }
}
