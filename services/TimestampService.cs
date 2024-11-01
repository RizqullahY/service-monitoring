using System;
using System.IO;

namespace MonitoringSystemApp.Services
{
   public class TimestampService
   {
      public void WriteTimestampToFile(string filePath)
      {
            DateTime fetchTime = DateTime.Now; 
            try
            {
               using (StreamWriter writer = new StreamWriter(filePath, append: true))
               {
                  writer.WriteLine("\n\n======= Data Fetch Timestamp ======== 🚀🚀🚀");
                  writer.WriteLine("Fetch Time: " + fetchTime);
                  writer.WriteLine("=====================================\n\n");
               }
               Console.WriteLine("Timestamp written to file: " + filePath);
            }
            catch (Exception ex)
            {
               Console.WriteLine("Error writing timestamp to file: " + ex.Message);
            }
      }
   }
}
