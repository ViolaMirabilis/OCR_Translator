namespace OCR_Translator.Model;

public class LibreTranslateRequest
{
    public string q { get; set; }
    public string source { get; set; } = string.Empty;
    public string target { get; set; } = string.Empty;
    public string format { get; set; } = "text";
    public int alternatives { get; set; } = 0;
    public string api_key { get; set; } = string.Empty;
}
