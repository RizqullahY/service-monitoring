using System;
using System.Diagnostics;

namespace MonitoringSystemApp.Services
{
   public class WiFiService
   {
      public void GetWiFiStatus()
      {
            ProcessStartInfo psi = new ProcessStartInfo
            {
               FileName = "netsh",
               Arguments = "wlan show interfaces",
               RedirectStandardOutput = true,
               UseShellExecute = false,
               CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            using (var reader = process.StandardOutput)
            {
               string output = reader.ReadToEnd();
               Console.WriteLine("");
               Console.WriteLine("WiFi Status: ");
               Console.WriteLine(output);
            }
      }
   }
}
