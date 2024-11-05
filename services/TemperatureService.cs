using System.Management;
using MonitoringSystemApp.Models;

namespace MonitoringSystemApp.Services
{
   public class TemperatureService
   {
      public TemperatureInfo GetCpuTemperature()
      {
            double tempInCelsius = 0;
            try
            {
               ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

               foreach (ManagementObject obj in searcher.Get())
               {
                  tempInCelsius = (Convert.ToDouble(obj["CurrentTemperature"]) - 2732) / 10.0;
                  break;
               }
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error retrieving CPU temperature: " + ex.Message);
            }
            
            return new TemperatureInfo { Temperature = tempInCelsius };
      }
   }
}
