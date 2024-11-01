using System;
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
            Console.WriteLine("");
            Console.WriteLine("Start of time on : " + bootTime);
            Console.WriteLine("Has been started on : " + (DateTime.Now - bootTime));
            return DateTime.Now - bootTime;
      }
   }
}
