namespace MonitoringSystemApp.Utilities
{
    public static class NetworkHandler
    {
        public static bool IsInternetAvailable()
        {
            try
            {
                using var client = new HttpClient();
                var response = client.GetAsync("http://www.google.com").GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
