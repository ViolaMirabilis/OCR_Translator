namespace OCR_Translator.Interfaces;

public interface IOverlaySettings
{
    public int TextBoxFontSize { get; set; }

    public string TextBoxColor { get; set; }

    public string TextColor { get; set; }

    public int GameWidth { get; set; }

    public int GameHeight { get; set; }
    public string TranslateFrom { get; set; }
    public string TranslateTo { get; set; }
    public string ApiKey { get; set; }
}
