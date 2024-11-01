using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MonitoringSystemApp.Models;
using MonitoringSystemApp.Services;

namespace MonitoringSystemApp
{
   class Program
   {
      static async Task Main(string[] args)
      {
            Console.WriteLine("Mengambil data sistem...");

            var batteryService = new BatteryService();
            var systemInfoService = new SystemInfoService();
            var wifiService = new WiFiService();
            var TemperatureService = new TemperatureService();

            // Ambil data dari masing-masing layanan
            var batteryInfo = batteryService.GetBatteryStatus();
            var systemInfo = systemInfoService.GetSystemInfo();
            var wifiInfo = wifiService.GetWiFiStatus();
            var TemperatureInfo = TemperatureService.GetCpuTemperature();

            // Gabungkan semua data dalam satu objek SystemData
            var systemData = new SystemData
            {
               BatteryInfo = batteryInfo,
               SystemInfo = systemInfo,
               WiFiInfo = wifiInfo,
               TemperatureInfo = TemperatureInfo
            };

            Console.WriteLine("Pengambilan data selesai.");
            Console.WriteLine("Menulis data sistem ke file JSON...");

            string filePath = "SystemInfoLog.json";

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
      }
   }
}
