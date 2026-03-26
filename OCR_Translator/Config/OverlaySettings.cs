using OCR_Translator.Interfaces;
using System.Configuration;

namespace OCR_Translator.Config;

public class OverlaySettings : ConfigurationSection, IOverlaySettings
{
    // this["PropertyName"] grabs the value from a given property. It needs to be casted to a desired type.
    [ConfigurationProperty(nameof(TextBoxFontSize), DefaultValue = 8)]
    public int TextBoxFontSize
    {
        get { return (int)this[nameof(TextBoxFontSize)]; }
        set { this[nameof(TextBoxFontSize)] = value; }
    }

    [ConfigurationProperty(nameof(TextBoxColor), DefaultValue = "#FFFFFF")]
    public string TextBoxColor
    {
        get { return (string)this[nameof(TextBoxColor)]; }
        set { this[nameof(TextBoxColor)] = value; }
    }

    [ConfigurationProperty(nameof(TextColor), DefaultValue = "#000000")]
    public string TextColor
    {
        get { return (string)this[nameof(TextColor)]; }
        set { this[nameof(TextColor)] = value; }
    }

    [ConfigurationProperty(nameof(GameWidth), DefaultValue = 1920)]
    public int GameWidth
    {
        get { return (int)this[nameof(GameWidth)]; }
        set { this[nameof(GameWidth)] = value; }
    }

    [ConfigurationProperty(nameof(GameHeight), DefaultValue = 1080)]
    public int GameHeight
    {
        get { return (int)this[nameof(GameHeight)]; }
        set { this[nameof(GameHeight)] = value; }
    }

    [ConfigurationProperty(nameof(TranslateFrom), DefaultValue = "TH-th")]
    public string TranslateFrom
    {
        get { return (string)this[nameof(TranslateFrom)]; }
        set { this[nameof(TranslateFrom)] = value; }
    }

    [ConfigurationProperty(nameof(TranslateTo), DefaultValue = "En-en")]
    public string TranslateTo
    {
        get { return (string)this[nameof(TranslateTo)]; }
        set { this[nameof(TranslateTo)] = value; }
    }

    [ConfigurationProperty(nameof(ApiKey), DefaultValue = "YOUR-API-KEY-GOES-HERE")]
    public string ApiKey
    {
        get { return (string)this[nameof(ApiKey)]; }
        set { this[nameof(ApiKey)] = value; }
    }
}
