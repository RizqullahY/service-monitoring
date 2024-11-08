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
        private readonly TokenService _tokenService;  
        private readonly string _authToken;
        private const string DirectoryPath = @"C:\MonitoringSystem\QueueData";
        private const string ApiUrl = "http://localhost:8000/api/system-info";
        
        public SystemMonitoringService(TokenService tokenService) 
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _authToken = _tokenService.GetAuthToken() ?? string.Empty;  
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_authToken == null)
            {
                return; 
            }

            // FileHandler.EnsureDirectoryExists(DirectoryPath); 

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Mengambil data sistem...");

                var batteryService = new BatteryService();
                var systemInfoService = new SystemInfoService();
                var wifiService = new WiFiService();
                var temperatureService = new TemperatureService();
                var diskService = new DiskService();

                var batteryInfo = batteryService.GetBatteryStatus();
                var systemInfo = systemInfoService.GetSystemInfo();
                var wifiInfo = wifiService.GetWiFiStatus();
                var temperatureInfo = temperatureService.GetCpuTemperature();
                
                var diskInfo = diskService.GetTotalDiskStatus();


                DateTime now = DateTime.Now;

                var systemData = new SystemData
                {
                    DeviceName = Environment.MachineName,
                    BatteryInfo = batteryInfo,
                    SystemInfo = systemInfo,
                    DiskInfo = diskInfo,
                    WiFiInfo = wifiInfo,
                    TemperatureInfo = temperatureInfo,
                    Created_At = now.ToString("dd-MMMM-yyyy HH:mm:ss"),
                    SendToApiOnTime = NetworkHandler.IsInternetAvailable() && await _apiHandler.IsApiAvailable(ApiUrl)
                };

                Console.WriteLine("Pengambilan data selesai.");
                Console.WriteLine("Menulis data sistem ke file JSON...");

                string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });

                try
                {

                    if (NetworkHandler.IsInternetAvailable() && await _apiHandler.IsApiAvailable(ApiUrl))
                    {
                        await ProcessQueue(stoppingToken);
                        await _apiHandler.SendDataToApi(ApiUrl, jsonString, _authToken);
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
            string queueFilePath = Path.Combine(DirectoryPath, $"SystemDataQueue_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json");
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
                    await _apiHandler.SendDataToApi(ApiUrl, jsonString, _authToken);
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
