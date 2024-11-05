using System.Management;
using System.Text.Json;

namespace MonitoringSystemApp.Services
{
   public class BatteryService
   {
      public class BatteryInfo
      {
         public int BatteryStatus { get; set; }        
         public string? EstimatedChargeRemaining { get; set; }
         public string? EstimatedRunTime { get; set; }
         public bool IsCharging { get; set; }

         public string BatteryStatusDescription
         {
            get
            {
                  return BatteryStatus switch
                  {
                     1 => "Discharging (sedang digunakan)",
                     2 => "Connected and charging (sedang diisi)",
                     3 => "Fully charged (sudah penuh)",
                     _ => "Status tidak dikenal"
                  };
            }
         }

      }

      public BatteryInfo GetBatteryStatus()
      {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
            BatteryInfo batteryInfo = new BatteryInfo();

            foreach (ManagementObject obj in searcher.Get())
            {
               batteryInfo.BatteryStatus = Convert.ToInt32(obj["BatteryStatus"]);

               // estimasi persentase daya baterai yang tersisa
               batteryInfo.EstimatedChargeRemaining = $"{Convert.ToInt32(obj["EstimatedChargeRemaining"])}%";
               
               // estimasi waktu yang tersisa sampai baterai habis
               int estimatedRunTimeInMinutes = Convert.ToInt32(obj["EstimatedRunTime"]);
                if (estimatedRunTimeInMinutes > 0)
                {
                    TimeSpan timeSpan = TimeSpan.FromMinutes(estimatedRunTimeInMinutes);
                    batteryInfo.EstimatedRunTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                }
                else
                {
                    batteryInfo.EstimatedRunTime = "0";
                }

               batteryInfo.IsCharging = batteryInfo.BatteryStatus == 2; 
               /*
               1. Discharging = sedang digunakan
               2. Connected = sedang diisi dan di gunakan
               3. Fully = Sudah penuh
               */ 
            }

            return batteryInfo;
      }

   }
}
