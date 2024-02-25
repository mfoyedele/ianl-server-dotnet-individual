using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Repositories;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(TestAsyncActionFilter))]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepo _repo;
        private readonly IMapper _mapper;

        public DevicesController(IDeviceRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceReadDto>>> GetAllDevices([FromHeader] bool flipSwitch)
        {
            var Devices = await _repo.GetAllDevices();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"--> The flip switch is: {flipSwitch}");
            Console.ResetColor();

            return Ok(_mapper.Map<IEnumerable<DeviceReadDto>>(Devices));
        }

        [HttpGet("{DeviceTypeId}", Name = "GetDeviceById")]
        public async Task<ActionResult<DeviceReadDto>> GetDeviceById(string DeviceTypeId)
        {
            var DeviceModel = await _repo.GetDeviceById(DeviceTypeId);
            if (DeviceModel != null)
            {
                return Ok(_mapper.Map<DeviceReadDto>(DeviceModel));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DeviceReadDto>> CreateDevice(DeviceCreateDto cmdCreateDto)
        {
            var DeviceModel = _mapper.Map<Devices>(cmdCreateDto);
            await _repo.CreateDevice(DeviceModel);
            await _repo.SaveChanges();

            var cmdReadDto = _mapper.Map<DeviceReadDto>(DeviceModel);
            
            Console.WriteLine($"Model State is: {ModelState.IsValid}");

            return CreatedAtRoute(nameof(GetDeviceById), new { DeviceTypeId = cmdReadDto.DeviceTypeId}, cmdReadDto);
        }

        //PATCH api/v1/Devices/{id}
        [HttpPatch("{DeviceTypeId}")]
        public async Task<ActionResult> PartialDeviceUpdate(string DeviceTypeId, JsonPatchDocument<DeviceUpdateDto> patchDoc)
        {
            var DeviceModelFromRepo = await _repo.GetDeviceById(DeviceTypeId);
            if(DeviceModelFromRepo == null)
            {
                return NotFound();
            }

            var DeviceToPatch = _mapper.Map<DeviceUpdateDto>(DeviceModelFromRepo);
            patchDoc.ApplyTo(DeviceToPatch, ModelState);

            if(!TryValidateModel(DeviceToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(DeviceToPatch, DeviceModelFromRepo);

            //await _repo.UpdateDevice(DeviceModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        //PUT api/Devices/{id}
        [HttpPut("{DeviceTypeId}")]
        public async Task<ActionResult> UpdateDevice(string DeviceTypeId, DeviceUpdateDto DeviceUpdateDto)
        {
            var DeviceModelFromRepo = await _repo.GetDeviceById(DeviceTypeId);
            if(DeviceModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(DeviceUpdateDto, DeviceModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

         //DELETE api/Devices/{id}
        [HttpDelete("{DeviceTypeId}")]
        public async Task<ActionResult> DeleteDevice(string DeviceTypeId)
        {
            var DeviceModelFromRepo = await _repo.GetDeviceById(DeviceTypeId);
            if(DeviceModelFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteDevice(DeviceModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}