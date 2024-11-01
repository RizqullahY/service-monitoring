using System;
using System.IO;
using System.Management;

namespace MonitoringSystemApp.Services
{
   public class SystemInfoService
   {
      public DateTime GetLastBootUpTime()
      {
            DateTime bootTime = DateTime.MinValue;
            try
            {
               ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem");
               foreach (ManagementObject mo in searcher.Get())
               {
                  string lastBootUpTime = mo["LastBootUpTime"].ToString();
                  bootTime = ManagementDateTimeConverter.ToDateTime(lastBootUpTime);
                  break;
               }
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error retrieving boot time: " + ex.Message);
            }
            return bootTime;
      }

      public TimeSpan GetSystemUptime()
      {
            DateTime bootTime = GetLastBootUpTime();
            return DateTime.Now - bootTime;
      }

      public void WriteDataToFile(string filePath)
      {
            DateTime bootTime = GetLastBootUpTime();
            TimeSpan uptime = GetSystemUptime();
            
            try
            {
               using (StreamWriter writer = new StreamWriter(filePath, append: true)) // 'append: true' untuk menambahkan data tanpa menimpa
               {
                  writer.WriteLine("Last Boot Time: " + bootTime);
                  writer.WriteLine("System Uptime: " + uptime);
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
