using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public interface IDeviceRepository
    {
        Task SaveChanges();
        Task<IEnumerable<Devices>> GetAllDevices();
        Task<Devices> DeviceTypeId(string DeviceId);
        Task PostData(Devices device);        
    }

    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;

        public DeviceRepository(DataContext context)
        {
            _context = context;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Devices>> GetAllDevices()
        {
            return await _context.Devices.ToListAsync();
        }
        public async Task<Devices> DeviceTypeId(string DeviceId)
        {
            return await _context.Devices!.FirstOrDefaultAsync(c => c.DeviceTypeId == DeviceId);
        }


        public async Task PostData(Devices device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }
            await _context.Devices!.AddAsync(device);
        }

    }
}