using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IDeviceRepo
    {
        Task SaveChanges();

        Task<IEnumerable<Devices>> GetAllDevices();
        Task<Devices> GetDeviceById(string DeviceTypeId);
        Task CreateDevice(Devices cmd);
    
        void DeleteDevice(Devices cmd);
    }
}