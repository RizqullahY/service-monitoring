using System;
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
   }
}
