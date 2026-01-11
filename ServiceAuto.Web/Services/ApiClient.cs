using System.Net.Http.Json;

namespace ServiceAuto.Web.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint)
    {
        var res = await _http.GetAsync(endpoint);
        if (!res.IsSuccessStatusCode) return new List<T>();
        return (await res.Content.ReadFromJsonAsync<List<T>>()) ?? new List<T>();
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var res = await _http.GetAsync(endpoint);
        if (!res.IsSuccessStatusCode) return default;
        return await res.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PostAsync<T>(string endpoint, object body)
    {
        var res = await _http.PostAsJsonAsync(endpoint, body);
        if (!res.IsSuccessStatusCode) return default;
        return await res.Content.ReadFromJsonAsync<T>();
    }

    public async Task<bool> PutAsync(string endpoint, object body)
    {
        var res = await _http.PutAsJsonAsync(endpoint, body);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        var res = await _http.DeleteAsync(endpoint);
        return res.IsSuccessStatusCode;
    }
}
