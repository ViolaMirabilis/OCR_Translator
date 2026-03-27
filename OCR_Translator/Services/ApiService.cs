using OCR_Translator.Model;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Text;
using System.Net.Http.Json;

namespace OCR_Translator.Services;

public class ApiService
{
    private readonly HttpClient _http;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiEndpoint { get; set; } = "https://vision.googleapis.com/v1/images:annotate?key=";

    public ApiService()
    {
        _http = new HttpClient();
    }
    public async Task<string> SendBase64Async(string serializedContent)
    {
        try
        {
            var response = await _http.PostAsync($"{ApiEndpoint}{ApiKey}", new StringContent(serializedContent, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("An error occured while sendint data to the Cloud Vision API");
            }

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        return string.Empty;
    }

    public void SetApiKey(string key)
    {
        ApiKey = key;
    }
}
