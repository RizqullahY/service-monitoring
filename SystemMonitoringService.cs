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

                var systemData = new SystemData
                {
                    DeviceName = Environment.MachineName,
                    BatteryInfo = batteryService.GetBatteryStatus(),
                    SystemInfo = systemInfoService.GetSystemInfo(),
                    WiFiInfo = wifiService.GetWiFiStatus(),
                    TemperatureInfo = temperatureService.GetCpuTemperature(),
                };

                string jsonString = JsonSerializer.Serialize(
                    systemData,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                string filePath = Path.Combine(DirectoryPath, $"SystemInfoLog_{DateTime.Now:yyyyMMdd_HHmmss}.json");
                try
                {
                    if (NetworkHandler.IsInternetAvailable() && await _apiHandler.IsApiAvailable(ApiUrl))
                    {
                        await SendAndProcessQueue(jsonString, filePath, stoppingToken);
                    }
                    else
                    {
                        SaveToQueue(jsonString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("Penulisan data selesai.");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task SendAndProcessQueue(string jsonString, string filePath, CancellationToken stoppingToken)
        {
            // Process queue and send the current file
            await _apiHandler.SendDataToApi(ApiUrl, jsonString);
            File.Delete(filePath);
        }

        private void SaveToQueue(string jsonString)
        {
            string queueFilePath = Path.Combine(
                DirectoryPath,
                $"SystemDataQueue_{DateTime.Now:yyyyMMdd_HHmmss}.json"
            );

            File.WriteAllText(queueFilePath, jsonString);
            Console.WriteLine($"Data JSON disimpan ke dalam antrian: " + queueFilePath);
        }
    }
}
