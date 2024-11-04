using System;
using System.IO;
using System.Net.Http;
using System.Text;
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
        private readonly HttpClient client = new HttpClient();
        private const string DirectoryPath = @"C:\MonitoringSystem";
        private const string ApiUrl = "http://localhost:8000/api/system-info";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EnsureDirectoryExists(DirectoryPath);
            CleanDirectory(DirectoryPath);

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

                string deviceName = Environment.MachineName;

                var systemData = new SystemData
                {
                    DeviceName = deviceName,
                    BatteryInfo = batteryInfo,
                    SystemInfo = systemInfo,
                    WiFiInfo = wifiInfo,
                    TemperatureInfo = temperatureInfo,
                };

                Console.WriteLine("Pengambilan data selesai.");
                Console.WriteLine("Menulis data sistem ke file JSON...");

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filePath = Path.Combine(DirectoryPath, $"SystemInfoLog_{timestamp}.json");

                string jsonString = JsonSerializer.Serialize(
                    systemData,
                    new JsonSerializerOptions { WriteIndented = true }
                );

                try
                {
                  //   File.WriteAllText(filePath, jsonString);
                    Console.WriteLine("Data sistem berhasil ditulis ke file JSON: " + filePath);

                    if (IsInternetAvailable() && await IsApiAvailable())
                    {
                        await ProcessQueue(stoppingToken); // Proses antrean sebelum mengirim data terbaru
                        await SendDataToApi(jsonString);
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

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Direktori dibuat: " + path);
            }
            else
            {
                Console.WriteLine("Direktori sudah ada: " + path);
            }
        }

        private void CleanDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine("File dihapus: " + file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error menghapus file: " + ex.Message);
                }
            }
        }

        private async Task<bool> IsApiAvailable()
        {
            try
            {
                var response = await client.GetAsync(ApiUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                using var pingClient = new HttpClient();
                var response = pingClient.GetAsync("http://www.google.com").GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async Task SendDataToApi(string jsonString)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data berhasil dikirim ke API.");
            }
            else
            {
                Console.WriteLine("Gagal mengirim data. Status code: " + response.StatusCode);
               //  SaveToQueue(jsonString);
            }
        }

        private void SaveToQueue(string jsonString)
        {
            string queueFilePath = Path.Combine(
                DirectoryPath,
                $"SystemDataQueue_{DateTime.Now:yyyyMMdd_HHmmss}.json"
            );

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
                    await SendDataToApi(jsonString);
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
