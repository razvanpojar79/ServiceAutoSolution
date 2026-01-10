using System.Net.Http.Json;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                             ? "http://10.0.2.2:5000"
                             : "http://localhost:5000";

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<T>>(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare conexiune: {ex.Message}");
                return new List<T>();
            }
        }
    }
}