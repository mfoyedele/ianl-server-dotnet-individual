using AutoMapper;

using WebApi.Entities;
using WebApi.Models.Devices;

namespace WebApi.Profiles
{
    public class DevicesProfile : Profile
    {
        public DevicesProfile()
        {
            // Source -> Target
            CreateMap<Devices, DeviceReadDto>();
            CreateMap<DeviceCreateDto, Devices>();
            CreateMap<DeviceUpdateDto, Devices>();
            CreateMap<Devices, DeviceUpdateDto>();
        }
    }
}