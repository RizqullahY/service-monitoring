using System;
using System.IO;
using System.Management;
using System.Text.Json;

namespace MonitoringSystemApp.Services
{
   public class BatteryService
   {
      public class BatteryInfo
      {
         public int BatteryStatus { get; set; }
         public int EstimatedChargeRemaining { get; set; }
         public int EstimatedRunTime { get; set; }
      }

      public BatteryInfo GetBatteryStatus()
      {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
            BatteryInfo batteryInfo = new BatteryInfo();

            foreach (ManagementObject obj in searcher.Get())
            {
               batteryInfo.BatteryStatus = Convert.ToInt32(obj["BatteryStatus"]);
               batteryInfo.EstimatedChargeRemaining = Convert.ToInt32(obj["EstimatedChargeRemaining"]);
               batteryInfo.EstimatedRunTime = Convert.ToInt32(obj["EstimatedRunTime"]);
            }

            return batteryInfo;
      }

      public void WriteDataToFile(string filePath)
      {
            BatteryInfo batteryInfo = GetBatteryStatus();

            // Serialize data ke format JSON dengan indented output
            string jsonString = JsonSerializer.Serialize(batteryInfo, new JsonSerializerOptions { WriteIndented = true });

            if (File.Exists(filePath))
            {
               File.AppendAllText(filePath, jsonString + Environment.NewLine);
            }
            else
            {
               File.WriteAllText(filePath, jsonString + Environment.NewLine);
            }

            Console.WriteLine("Data baterai berhasil ditulis ke file dalam format JSON: " + filePath);
      }
   }
}
