using System;
using System.Diagnostics;
using System.IO;

namespace MonitoringSystemApp.Services
{
   public class WiFiService
   {
      public string GetWiFiStatus()
      {
            string output = "";
            try
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
                  output = reader.ReadToEnd();
               }
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error retrieving WiFi status: " + ex.Message);
            }

            return output; 
      }

      public void WriteDataToFile(string filePath)
      {
            string wifiStatus = GetWiFiStatus(); 
            
            try
            {
               using (StreamWriter writer = new StreamWriter(filePath, append: true)) // 'append: true' untuk menambahkan data tanpa menimpa
               {
                  writer.WriteLine("WiFi Status:");
                  writer.WriteLine(wifiStatus);
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
