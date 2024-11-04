namespace MonitoringSystemApp.Utilities
{
    public static class FileHandler
    {
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Direktori dibuat: " + path);
            }
            else
            {
                Console.WriteLine("Direktori sudah ada: " + path);
            }
        }

        public static void CleanDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine("File dihapus: " + file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error menghapus file: " + ex.Message);
                }
            }
        }
    }
}
