using OCR_Translator.Model;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

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
    public async Task SendBase64Async(string serializedContent)
    {
        try
        {
            var response = await _http.PostAsync($"ApiEndpoint{ApiKey}", new StringContent(serializedContent));

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("An error occured while sendint data to the Cloud Vision API");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        // to do
    }

    public void SetApiKey(string key)
    {
        ApiKey = key;
    }
}
