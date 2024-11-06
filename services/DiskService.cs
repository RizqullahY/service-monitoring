using System.Management;

namespace MonitoringSystemApp.Services
{
    public class DiskService
    {
        public class DiskInfo
        {
            public string? TotalSize { get; set; }
            public string? FreeSpace { get; set; }
            public string? UsedSpace { get; set; }
            public string? PercentUsed { get; set; }
        }

        public Dictionary<string, DiskInfo> GetDiskStatus()
        {
            Dictionary<string, DiskInfo> diskInfos = new Dictionary<string, DiskInfo>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3");

            foreach (ManagementObject obj in searcher.Get())
            {
                string diskName = obj["DeviceID"]?.ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(diskName)) continue; 

                DiskInfo diskInfo = new DiskInfo
                {
                    TotalSize = FormatSize(Convert.ToInt64(obj["Size"])), 
                    FreeSpace = FormatSize(Convert.ToInt64(obj["FreeSpace"])), 
                };

                long totalSize = Convert.ToInt64(obj["Size"]);
                long freeSpace = Convert.ToInt64(obj["FreeSpace"]);
                long usedSpace = totalSize - freeSpace;

                diskInfo.UsedSpace = FormatSize(usedSpace); 
                diskInfo.PercentUsed = (totalSize > 0 ? Math.Floor((double)usedSpace / totalSize * 100) : 0) + " %"; 
                diskInfos[diskName] = diskInfo;
            }

            return diskInfos; 
        }

        private string FormatSize(long sizeInBytes)
        {
            if (sizeInBytes >= 1 << 30)
                return $"{sizeInBytes / (1 << 30)} GB";
            if (sizeInBytes >= 1 << 20)
                return $"{sizeInBytes / (1 << 20)} MB";
            if (sizeInBytes >= 1 << 10)
                return $"{sizeInBytes / (1 << 10)} KB";
            return $"{sizeInBytes} Bytes";
        }
    }
}
