using System.Diagnostics;
using System.Text.Json;
using MonitoringSystemApp.Models;  

namespace MonitoringSystemApp.Services
{
   public class WiFiService
   {
      public WiFiInfo GetWiFiStatus()
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

            return ParseWiFiInfo(output); // Mengurai data hasil netsh ke dalam WiFiInfo
      }

      private WiFiInfo ParseWiFiInfo(string output)
      {
            WiFiInfo wifiInfo = new WiFiInfo();
            StringReader reader = new StringReader(output);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
               if (line.Contains("SSID") && !line.Contains("BSSID"))
                  wifiInfo.SSID = line.Split(':')[1].Trim();
               else if (line.Contains("State"))
                  wifiInfo.State = line.Split(':')[1].Trim();
               else if (line.Contains("Signal"))
                  wifiInfo.SignalQuality = line.Split(':')[1].Trim();
               else if (line.Contains("Radio type"))
                  wifiInfo.RadioType = line.Split(':')[1].Trim();
               else if (line.Contains("Authentication"))
                  wifiInfo.Authentication = line.Split(':')[1].Trim();
            }

            return wifiInfo;
      }

      public void WriteDataToFile(string filePath)
      {
            WiFiInfo wifiInfo = GetWiFiStatus();
            if (wifiInfo == null)
            {
               Console.WriteLine("WiFi information could not be retrieved.");
               return;
            }

            try
            {
               string jsonString = JsonSerializer.Serialize(wifiInfo, new JsonSerializerOptions { WriteIndented = true });

               if (File.Exists(filePath))
               {
                  File.AppendAllText(filePath, jsonString);
               }
               else
               {
                  File.WriteAllText(filePath, jsonString);
               }

               Console.WriteLine("WiFi data written to file: " + filePath);
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error writing to file: " + ex.Message);
            }
      }
   }
}
