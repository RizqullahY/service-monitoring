using System;
using System.Threading.Tasks;
using MonitoringSystemApp.Services;

namespace MonitoringSystemApp
{
   class Program
   {
      static async Task Main(string[] args)
      {
            Console.WriteLine("Fetching system data...");

            var batteryService = new BatteryService();
            var wifiService = new WiFiService();
            var temperatureService = new TemperatureService();
            var systemInfoService = new SystemInfoService();

            // batteryService.GetBatteryStatus();
            // wifiService.GetWiFiStatus();
            // temperatureService.GetCpuTemperature();
            // systemInfoService.GetSystemUptime();

            Console.WriteLine("");
            Console.WriteLine("Data fetching completed.");

            Console.WriteLine("");
            Console.WriteLine("Writting system data ...");
            
            string filePath = "SystemInfoLog.txt"; 
            
            TimestampService timestampService = new TimestampService();
            timestampService.WriteTimestampToFile(filePath);
            
            temperatureService.WriteDataToFile(filePath);
            wifiService.WriteDataToFile(filePath);
            batteryService.WriteDataToFile(filePath);
            systemInfoService.WriteDataToFile(filePath);

            Console.WriteLine("Data writing completed.");
      }
   }
}
