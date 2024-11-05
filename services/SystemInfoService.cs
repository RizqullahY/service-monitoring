using System.Management;

namespace MonitoringSystemApp.Services
{
   public class SystemInfoService
   {
      public class SystemInfo
      {
         public DateTime LastBootTime { get; set; }
         public TimeSpan SystemUptime { get; set; }
      }

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

      public SystemInfo GetSystemInfo()
      {
            DateTime bootTime = GetLastBootUpTime();
            TimeSpan uptime = DateTime.Now - bootTime;

            // Mengembalikan informasi sistem sebagai objek SystemInfo
            return new SystemInfo
            {
               LastBootTime = bootTime,
               SystemUptime = uptime
            };
      }
   }
}
