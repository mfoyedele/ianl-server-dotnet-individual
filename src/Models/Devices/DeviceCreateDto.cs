using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Devices
{
    public class DeviceCreateDto
    {
        [Required]
        public string DeviceTypeId { get; set; }  
        [Required]  
        public string Device { get; set; }
        [Required]
        public int Time {get; set; }
        [Required]
        public int SeqNumber {get; set; }
        [Required]
        public string Data {get; set; }
    }
    
}