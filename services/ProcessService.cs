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
