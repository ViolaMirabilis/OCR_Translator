using OCR_Translator.Config;
using OCR_Translator.Interfaces;
using System.Configuration;
using System.Drawing.Text;

namespace OCR_Translator.Services;

/// <summary>
/// Config service responsible for saving and retrieving data from and to the config file.
/// Takes in a class that inherits from IOverlaySettings
/// </summary>
public class ConfigService
{
    // backing field
    private readonly Configuration _config;

    // assigning value to the backing field on initialization (singleton basically)
    public ConfigService()
    {
        _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        // if config doesn't exist, initialize it.
        if (_config.Sections["OverlaySettings"] is null)
        {
            _config.Sections.Add(nameof(OverlaySettings), new OverlaySettings());
        }

    }

    // loads config data from the UI to the file
    public void LoadConfig(IOverlaySettings config)
    {
        var OverlaySettingsSection = (OverlaySettings)_config.Sections[nameof(OverlaySettings)];
        config.TextBoxFontSize = OverlaySettingsSection.TextBoxFontSize;
        config.TextBoxColor = OverlaySettingsSection.TextBoxColor;
        config.TextColor = OverlaySettingsSection.TextColor;
        config.GameWidth = OverlaySettingsSection.GameWidth;
        config.GameHeight = OverlaySettingsSection.GameHeight;
        config.TranslateFrom = OverlaySettingsSection.TranslateFrom;
        config.TranslateTo = OverlaySettingsSection.TranslateTo;
        config.ApiKey = OverlaySettingsSection.ApiKey;

    }

    // get's data from the config file and assigns it to the UI
    public void SaveConfig(IOverlaySettings config)
    {
        var OverlaySettingsSection = (OverlaySettings)_config.Sections[nameof(OverlaySettings)];
        OverlaySettingsSection.TextBoxFontSize = config.TextBoxFontSize;
        OverlaySettingsSection.TextBoxColor = config.TextBoxColor;
        OverlaySettingsSection.TextColor = config.TextColor;
        OverlaySettingsSection.GameWidth = config.GameWidth;
        OverlaySettingsSection.GameHeight = config.GameHeight;
        OverlaySettingsSection.TranslateFrom = config.TranslateFrom;
        OverlaySettingsSection.TranslateTo = config.TranslateTo;
        OverlaySettingsSection.ApiKey = config.ApiKey;

        _config.Save();
    }
}
