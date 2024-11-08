using System.Text;
using System.Net;
using System.Net.Http.Headers;

namespace MonitoringSystemApp.Utilities
{
    public class ApiHandler
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<bool> IsApiAvailable(string apiUrl)
        {
            try
            {
                var response = await _client.GetAsync(apiUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task SendDataToApi(string apiUrl, string jsonString, string authToken)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(authToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authToken);
            }

            var response = await _client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data berhasil dikirim ke API.");
            }
            else
            {
                Console.WriteLine("Gagal mengirim data. Status code: " + response.StatusCode);
            }
        }
    }
}
