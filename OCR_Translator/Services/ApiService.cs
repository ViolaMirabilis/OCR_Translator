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
    /// <summary>
    /// Cloud Vision API endpoints
    /// </summary>
    /// <param name="serializedContent"></param>
    /// <returns></returns>
    public async Task<string> SendBase64Async(string serializedContent)
    {
        try
        {
            var response = await _http.PostAsync($"{ApiEndpoint}{ApiKey}", new StringContent(serializedContent, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("An error occured while sending data to the Cloud Vision API");
            }

            string result = await response.Content.ReadAsStringAsync();
            System.IO.File.WriteAllText(@"C:\Users\zajac\Desktop\test_parse.txt", result);
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

    public async Task<string> TranslateText(string allLinesCombined, string sourceLanguage, string targetLanguage)
    {
        // creating a new request, specifying content, source and target language
        LibreTranslateRequest request = new LibreTranslateRequest {q = allLinesCombined, source = sourceLanguage, target = targetLanguage};
        var serializedRequest = JsonSerializer.Serialize(request);
        try
        {
            var response = await _http.PostAsync("http://127.0.0.1:5000/translate", new StringContent(serializedRequest, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("An error occured while sending data to the LibreTranslate API");
            }

            string result = await response.Content.ReadAsStringAsync();
            //var deserialized = JsonSerializer.Deserialize<LibreTranslateResponse>(result);
            //var deserializedText = deserialized.TranslatedText;
            //System.IO.File.WriteAllText(@"C:\Users\zajac\Desktop\lines.txt", deserialized.TranslatedText);
            System.IO.File.WriteAllText(@"C:\Users\zajac\Desktop\lines.txt", result);
            return result;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        return string.Empty;
    }


}
