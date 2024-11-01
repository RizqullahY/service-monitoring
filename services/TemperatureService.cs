using System;
using System.IO;
using System.Management;

namespace MonitoringSystemApp.Services
{
   public class TemperatureService
   {
      public double GetCpuTemperature()
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
            return tempInCelsius;
      }

      public void WriteDataToFile(string filePath)
      {
            double cpuTemperature = GetCpuTemperature();
            
            try
            {
               using (StreamWriter writer = new StreamWriter(filePath, append: true)) // 'append: true' untuk menambahkan data tanpa menimpa
               {
                  writer.WriteLine("=====================================");
                  writer.WriteLine("CPU Temperature: " + cpuTemperature + " °C");
                  writer.WriteLine("=====================================");
               }

               Console.WriteLine("Data written to file: " + filePath);
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error writing to file: " + ex.Message);
            }
      }
   }
}
