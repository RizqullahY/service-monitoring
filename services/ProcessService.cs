using System.Diagnostics;

namespace MonitoringSystemApp.Services
{
   public class ProcessService
   {
      public void GetRunningProcesses()
      {
            // Mengambil daftar proses yang sedang berjalan
            var processes = Process.GetProcesses();
            
            Console.WriteLine("Aplikasi yang sedang berjalan dan penggunaan CPU:");
            Console.WriteLine("==============================================");

            foreach (var process in processes)
            {
               try
               {
                  // Mengambil informasi CPU dan memori dari setiap proses
                  var cpuUsage = GetCpuUsage(process);
                  var memoryUsage = process.WorkingSet64 / (1024 * 1024); // Konversi ke MB

                  Console.WriteLine($"Process Name: {process.ProcessName}");
                  Console.WriteLine($"CPU Usage: {cpuUsage}%");
                  Console.WriteLine($"Memory Usage: {memoryUsage} MB");
                  Console.WriteLine("----------------------------------------------");
               }
               catch
               {
                  // Lewati proses yang mungkin tidak dapat diakses
                  continue;
               }
            }
      }

      public void WriteDataToFile(string filePath)
      {
            var processes = Process.GetProcesses();
            string processData = "";

            foreach (var process in processes)
            {
               try
               {
                  var cpuUsage = GetCpuUsage(process);
                  var memoryUsage = process.WorkingSet64 / (1024 * 1024); // Dalam MB

                  processData += $"Process Name: {process.ProcessName}\n";
                  processData += $"CPU Usage: {cpuUsage}%\n";
                  processData += $"Memory Usage: {memoryUsage} MB\n";
                  processData += "----------------------------------------------\n";
               }
               catch
               {
                  continue; // Lewati proses yang tidak bisa diakses
               }
            }

            // Tulis data proses ke file
            if (File.Exists(filePath))
            {
               File.AppendAllText(filePath, processData);
            }
            else
            {
               File.WriteAllText(filePath, processData);
            }

            Console.WriteLine("Data proses ditulis ke file: " + filePath);
      }

      // Metode untuk menghitung penggunaan CPU dari sebuah proses
      private float GetCpuUsage(Process process)
      {
            try
            {
               using (PerformanceCounter pc = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true))
               {
                  pc.NextValue(); // Pertama kali untuk initialize
                  Task.Delay(500).Wait(); // Menunggu setengah detik untuk mengambil nilai penggunaan CPU

                  return pc.NextValue() / Environment.ProcessorCount;
               }
            }
            catch
            {
               return 0; // Jika tidak dapat menghitung, kembalikan 0
            }
      }
   }
}
