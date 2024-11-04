using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http; 
using Microsoft.Extensions.Hosting;
using MonitoringSystemApp.Models;
using MonitoringSystemApp.Services;

namespace MonitoringSystemApp
{
   public class SystemMonitoringService : BackgroundService
   {
      private readonly HttpClient client = new HttpClient();

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

               string deviceName = Environment.MachineName;
               // Gabungkan semua data dalam satu objek SystemData
               var systemData = new SystemData
               {
                  DeviceName = deviceName,
                  BatteryInfo = batteryInfo,
                  SystemInfo = systemInfo,
                  WiFiInfo = wifiInfo,
                  TemperatureInfo = temperatureInfo
               };

               Console.WriteLine("Pengambilan data selesai.");
               Console.WriteLine("Menulis data sistem ke file JSON...");

               // Membuat nama file JSON berdasarkan timestamp
               string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); 
               string filePath = $@"C:\MonitoringSystem\SystemInfoLog_{timestamp}.json";

               // Serialisasi seluruh data menjadi satu file JSON
               string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });

               try
               {
                  // Menulis data ke file JSON
                  File.WriteAllText(filePath, jsonString);
                  Console.WriteLine("Data sistem berhasil ditulis ke file JSON: " + filePath);

                  // Mengirim data JSON ke API Laravel
                  var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                  var url = "http://localhost:8000/api/system-info";
                  
                  var response = await client.PostAsync(url, content);
                  if (response.IsSuccessStatusCode)
                  {
                        Console.WriteLine("Data berhasil dikirim ke API Laravel.");
                  }
                  else
                  {
                        Console.WriteLine("Gagal mengirim data. Status code: " + response.StatusCode);
                  }
               }
               catch (Exception ex)
               {
                  Console.WriteLine("Error menulis ke file atau mengirim data: " + ex.Message);
               }

               Console.WriteLine("Penulisan data selesai.");

               // Tunggu selama beberapa waktu sebelum mengambil data lagi
               await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
      }
   }
}
