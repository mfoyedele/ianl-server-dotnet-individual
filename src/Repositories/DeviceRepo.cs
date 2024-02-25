using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public class DeviceRepo : IDeviceRepo
    {
        private readonly DataContext _context;

        public DeviceRepo(DataContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Devices>> GetAllDevices()
        {
            return await _context.Devices!.ToListAsync();
        }
        public async Task CreateDevice(Devices cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            await _context.Devices!.AddAsync(cmd);
        }

        public async Task<Devices?> GetDeviceById(string id)
        {
            return await _context.Devices!.FirstOrDefaultAsync(c => c.DeviceTypeId == id);
        }
           public void DeleteDevice(Devices cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Devices.Remove(cmd);
        }



    }
}

