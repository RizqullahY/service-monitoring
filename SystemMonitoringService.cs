using System.Text.Json;
using Microsoft.Extensions.Hosting;
using MonitoringSystemApp.Models;
using MonitoringSystemApp.Services;
using MonitoringSystemApp.Utilities;

namespace MonitoringSystemApp
{
    public class SystemMonitoringService : BackgroundService
    {
        private readonly ApiHandler _apiHandler = new ApiHandler();
        private const string DirectoryPath = @"C:\MonitoringSystem";
        private const string ApiUrl = "http://localhost:8000/api/system-info";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            FileHandler.EnsureDirectoryExists(DirectoryPath);
            FileHandler.CleanDirectory(DirectoryPath);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Mengambil data sistem...");

                var batteryService = new BatteryService();
                var systemInfoService = new SystemInfoService();
                var wifiService = new WiFiService();
                var temperatureService = new TemperatureService();

                var batteryInfo = batteryService.GetBatteryStatus();
                var systemInfo = systemInfoService.GetSystemInfo();
                var wifiInfo = wifiService.GetWiFiStatus();
                var temperatureInfo = temperatureService.GetCpuTemperature();

                DateTime now = DateTime.Now;

                var systemData = new SystemData
                {
                    DeviceName = Environment.MachineName,
                    BatteryInfo = batteryInfo,
                    SystemInfo = systemInfo,
                    WiFiInfo = wifiInfo,
                    TemperatureInfo = temperatureInfo,
                    Created_At = now.ToString("dd-MMMM-yyyy")
                };

                Console.WriteLine("Pengambilan data selesai.");
                Console.WriteLine("Menulis data sistem ke file JSON...");

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filePath = Path.Combine(DirectoryPath, $"SystemInfoLog_{timestamp}.json");

                string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });

                try
                {
                    Console.WriteLine("Data sistem berhasil ditulis ke file JSON: " + filePath);

                    if (NetworkHandler.IsInternetAvailable() && await _apiHandler.IsApiAvailable(ApiUrl))
                    {
                        await ProcessQueue(stoppingToken);
                        await _apiHandler.SendDataToApi(ApiUrl, jsonString);
                        File.Delete(filePath);
                        Console.WriteLine("File data berhasil dikirim dan dihapus: " + filePath);
                    }
                    else
                    {
                        SaveToQueue(jsonString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error menulis ke file atau mengirim data: " + ex.Message);
                }

                Console.WriteLine("Penulisan data selesai.");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private void SaveToQueue(string jsonString)
        {
            string queueFilePath = Path.Combine(DirectoryPath, $"SystemDataQueue_{DateTime.Now:yyyyMMdd_HHmmss}.json");
            File.WriteAllText(queueFilePath, jsonString);
            Console.WriteLine($"Data JSON disimpan ke dalam antrian (file): " + queueFilePath);
        }

        private async Task ProcessQueue(CancellationToken stoppingToken)
        {
            var queueFiles = Directory.GetFiles(DirectoryPath, "SystemDataQueue_*.json");
            foreach (var queueFile in queueFiles)
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(queueFile, stoppingToken);
                    await _apiHandler.SendDataToApi(ApiUrl, jsonString);
                    File.Delete(queueFile);
                    Console.WriteLine("Data dari file antrian berhasil dikirim dan file dihapus: " + queueFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saat mengirim data dari antrian: " + ex.Message);
                }
            }
        }
    }
}
