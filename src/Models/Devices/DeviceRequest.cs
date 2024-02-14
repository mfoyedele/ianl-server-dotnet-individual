namespace WebApi.Models.Devices;

public class DeviceRequest
{    
    public string DeviceTypeId { get; set; }    
    public string Device { get; set; }
    public int Time {get; set; }
    public int SeqNumber {get; set; }
    public string Data {get; set; }

}