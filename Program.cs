using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MonitoringSystemApp
{
   public class Program
   {
      public static void Main(string[] args)
      {
            CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .UseWindowsService() // Agar berjalan sebagai Windows Service
               .ConfigureServices((hostContext, services) =>
               {
                  services.AddHostedService<SystemMonitoringService>();
               });
   }
}
