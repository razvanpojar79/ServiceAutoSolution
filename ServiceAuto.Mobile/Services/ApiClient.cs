using System.Net.Http.Json;

namespace ServiceAuto.Mobile.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<T>>(endpoint);
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}