using System;
using System.Management;

namespace MonitoringSystemApp.Services
{
   public class TemperatureService
   {
      public void GetCpuTemperature()
      {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

            foreach (ManagementObject obj in searcher.Get())
            {
               double tempInCelsius = (Convert.ToDouble(obj["CurrentTemperature"].ToString()) - 2732) / 10.0;
               Console.WriteLine("CPU Temperature: " + tempInCelsius + " °C");
            }
      }
   }
}
