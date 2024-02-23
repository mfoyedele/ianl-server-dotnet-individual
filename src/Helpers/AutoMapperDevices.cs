namespace WebApi.Helpers;

using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Devices;
public class AutoMapperDevices : Profile
{
    // mappings between model and entity objects
    public AutoMapperDevices()
    {
        CreateMap<DeviceRequest, Devices>();

        CreateMap<Devices, DeviceResponse>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;                    

                    return true;
                }
            ));
    }
}