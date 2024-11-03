using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MonitoringSystemApp.Models;
using MonitoringSystemApp.Services;

namespace MonitoringSystemApp
{
   public class SystemMonitoringService : BackgroundService
   {
      protected override async Task ExecuteAsync(CancellationToken stoppingToken)
      {
            while (!stoppingToken.IsCancellationRequested)
            {
               Console.WriteLine("Mengambil data sistem...");

               var batteryService = new BatteryService();
               var systemInfoService = new SystemInfoService();
               var wifiService = new WiFiService();
               var temperatureService = new TemperatureService();

               // Ambil data dari masing-masing layanan
               var batteryInfo = batteryService.GetBatteryStatus();
               var systemInfo = systemInfoService.GetSystemInfo();
               var wifiInfo = wifiService.GetWiFiStatus();
               var temperatureInfo = temperatureService.GetCpuTemperature();

               // Gabungkan semua data dalam satu objek SystemData
               var systemData = new SystemData
               {
                  BatteryInfo = batteryInfo,
                  SystemInfo = systemInfo,
                  WiFiInfo = wifiInfo,
                  TemperatureInfo = temperatureInfo
               };

               Console.WriteLine("Pengambilan data selesai.");
               Console.WriteLine("Menulis data sistem ke file JSON...");

               // string filePath = "SystemInfoLog.json";
               // string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemInfoLog.json");

               string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); 
               string filePath = $@"C:\MonitoringSystem\SystemInfoLog_{timestamp}.json";


               // Serialisasi seluruh data menjadi satu file JSON
               string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });
               try
               {
                  File.WriteAllText(filePath, jsonString);
                  Console.WriteLine("Data sistem berhasil ditulis ke file JSON: " + filePath);
               }
               catch (Exception ex)
               {
                  Console.WriteLine("Error menulis ke file: " + ex.Message);
               }

               Console.WriteLine("Penulisan data selesai.");

               // Tunggu selama beberapa waktu sebelum mengambil data lagi
               await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
      }
   }
}
