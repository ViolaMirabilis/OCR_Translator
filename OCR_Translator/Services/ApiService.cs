using OCR_Translator.Model;
using System.Net.Http;
using System.Text.Json;

namespace OCR_Translator.Services;

public class ApiService
{
    private readonly HttpClient _http;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiEndpoint { get; set; } = string.Empty;

    public ApiService(string key, string endpoint)
    {
        _http = new HttpClient();
        ApiKey = key;
        ApiEndpoint = endpoint;
    }
    public async Task SendBase64Async(string serializedContent)
    {
        var response = await _http.PostAsync($"ApiEndpoint{ApiKey}", new StringContent(serializedContent));

        // to do
    }
}
