using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Devices;
using WebApi.Repositories;

namespace WebApi.Controllers 
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class DevicesController : BaseController
   {
    private readonly IDeviceRepository _repo;
    private readonly IMapper _mapper;

    public DevicesController(IDeviceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetllDevices()
    {
        var devices = await _repo.GetAllDevices();
        return Ok(_mapper.Map<IEnumerable<DeviceResponse>>(devices));

    }

    [HttpGet("{DeviceTypeId}", Name = "GetDevice")]
    public async Task<ActionResult<DeviceResponse>> GetDevice(string DeviceId)
    {
        var device = await _repo.DeviceTypeId(DeviceId);
        return Ok(_mapper.Map<DeviceResponse>(device));
    }

    [HttpPost]
    public async Task<ActionResult<DeviceResponse>> PostDevice(DeviceRequest model)
    {
        var deviceModel = _mapper.Map<Devices>(model);
        await _repo.PostData(deviceModel);
        await _repo.SaveChanges();

        var deviceReadDto = _mapper.Map<DeviceResponse>(deviceModel);

        return CreatedAtRoute(nameof(GetDevice), new { DeviceId = deviceReadDto.DeviceTypeId}, deviceReadDto);
    } 

   }

}