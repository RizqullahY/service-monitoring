using System;
using System.IO;
using System.Management;

namespace MonitoringSystemApp.Services
{
   public class BatteryService
   {
      public void GetBatteryStatus()
      {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");

            foreach (ManagementObject obj in searcher.Get())
            {
               Console.WriteLine("");
               Console.WriteLine("Battery Status: " + obj["BatteryStatus"]);
               Console.WriteLine("Battery Estimated Charge Remaining: " + obj["EstimatedChargeRemaining"] + "%");
               Console.WriteLine("Battery Estimated Run Time: " + obj["EstimatedRunTime"] + " minutes");
               Console.WriteLine("");
            }
      }

      public void WriteDataToFile(string filePath)
      {
            // Ambil informasi status baterai
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
            string batteryData = "";

            foreach (ManagementObject obj in searcher.Get())
            {
               batteryData += "Battery Status: " + obj["BatteryStatus"];
               batteryData += "\nBattery Estimated Charge Remaining: " + obj["EstimatedChargeRemaining"] + "%";
               batteryData += "\nBattery Estimated Run Time: " + obj["EstimatedRunTime"] + " minutes";
               batteryData += "\n=====================================\n";
            }

            if (File.Exists(filePath))
            {
               File.AppendAllText(filePath, batteryData);
            }
            else
            {
               File.WriteAllText(filePath, batteryData);
            }

            Console.WriteLine("Data written to file: " + filePath);
      }
   }
}
