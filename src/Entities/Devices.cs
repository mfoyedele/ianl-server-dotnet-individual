using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class Devices{
    [Key]
    public int Id { get; set; }
    public string DeviceTypeId { get; set; }    
    public string Device { get; set; }
    public int Time {get; set; }
    public int SeqNumber {get; set; }
    public string Data {get; set; }
    
}