using System.IO;
using System.Text.Json;

namespace MonitoringSystemApp.Services
{
    public class TokenService
    {
        private const string ConfigFilePath = @"C:\MonitoringSystem\config.conf";

        public string? GetAuthToken()
        {
            if (File.Exists(ConfigFilePath))
            {
                var configJson = File.ReadAllText(ConfigFilePath);
                var config = JsonSerializer.Deserialize<Config>(configJson);
                return config?.AuthToken; 
            }
            return null; 
        }
    }

    public class Config
    {
        public string? AuthToken { get; set; }
    }
}
