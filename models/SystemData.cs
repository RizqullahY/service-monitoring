using MonitoringSystemApp.Services;

namespace MonitoringSystemApp.Models
{
   public class SystemData
   {
      public string? DeviceName { get; set; }
      public BatteryService.BatteryInfo? BatteryInfo { get; set; }
      public SystemInfoService.SystemInfo? SystemInfo { get; set; }
      public WiFiInfo? WiFiInfo { get; set; }
      public TemperatureInfo? TemperatureInfo { get; set; }
   }
}