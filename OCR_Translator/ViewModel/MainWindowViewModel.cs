using OCR_Translator.Core;
using OCR_Translator.Interfaces;
using OCR_Translator.Model;
using OCR_Translator.Services;
using OCR_Translator.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static OCR_Translator.NativeWindowsHooks;

namespace OCR_Translator.ViewModel;

public class MainWindowViewModel : IOverlaySettings
{
    // Action event which is triggered once the "Submit" button is pressed
    public event Action OnSubmitClicked;
    #region Configuration
    private readonly Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    #endregion


    #region Services
    private readonly TranslationService _translationService;
    private OverlayWindow _overlayWindow;
    private readonly ConfigService _configService;
    private readonly ScreenshotService _screenshotService;
    #endregion

    #region Properties and fields
    // window's visibility. Starts with "true"
    private bool _isWindowVisible = true;
    public bool IsWindowVisible { get => _isWindowVisible; set { _isWindowVisible = value; OnPropertyChanged(); } }

    // overlay visibility variable
    private bool _isOverlayVisible = false;

    // source language
    private string _translateFrom = string.Empty;
    public string TranslateFrom { get => _translateFrom; set { _translateFrom = value; OnPropertyChanged(); } }
    
    // target language
    private string _translateTo = string.Empty;
    public string TranslateTo { get => _translateTo; set { _translateTo = value; OnPropertyChanged(); } }
    // bindable list of supported translation languages
    public ObservableCollection<Language> SupportedTranslationLanguages { get; set; }

    //Bindable font size
    private int _textBoxFontSize = 10;
    public int TextBoxFontSize { get => _textBoxFontSize; set { _textBoxFontSize = value; OnPropertyChanged(); } }

    // Bindable HEX for the textbox color
    private string _textBoxColor = "#FFFFFF";
    public string TextBoxColor { get => _textBoxColor; set { _textBoxColor = value; OnPropertyChanged(); } }

    // Bindable HEX for the text color
    private string _textColor = "#FFFFFF";
    public string TextColor { get => _textColor; set { _textColor = value; OnPropertyChanged(); } }

    // game's window width
    private int _gameWidth = 1920;
    public int GameWidth { get => _gameWidth; set { _gameWidth = value; OnPropertyChanged(); } }

    // game's window height
    private int _gameHeight = 1080;
    public int GameHeight { get => _gameHeight; set { _gameHeight = value; OnPropertyChanged(); } }

    // API KEY
    private string _apiKey = string.Empty;
    public string ApiKey { get => _apiKey; set { _apiKey = value; OnPropertyChanged(); } }
    #endregion

    #region Commands
    public RelayCommand SubmitConfigChanges { get; set; }
    #endregion


    public MainWindowViewModel()
    {
        _translationService = new TranslationService();
        _configService = new ConfigService();
        _screenshotService = new ScreenshotService();
        SubmitConfigChanges = new RelayCommand(_ => SubmitChanges(), _ => true);

        InitializeLanguagesCollection();
        InitializeConfigFile();
    }

    public void InitializeLanguagesCollection()
    {
        SupportedTranslationLanguages = _translationService.InitialiseLanguageCollection();
    }

    public void InitializeConfigFile()
    {
        _configService.LoadConfig(this);
    }
    
    #region First launch window settings
    public void ToggleOverlayVisibility()
    {
        // turns off the "settings" window once
        if (!_isOverlayVisible)
        {
            // if overlay is not visible, it creates a new object once and assigns it to the private variable
            _overlayWindow = new OverlayWindow(TextBoxFontSize, TextBoxColor, TextColor, GameWidth, GameHeight);
            // converts image to base64
            string base64 = _screenshotService.TakeScreenshot(GameWidth, GameHeight);
            // shows the overlay immediately
            _overlayWindow.Show();
            // makes the request to the API
            SendBase64ToApi(base64);

            _isOverlayVisible = true;
        }
        else
        {
            // it means the overlay window already exists, so it just toggles the view.
            _isOverlayVisible = false;
            _overlayWindow.Hide();
        }

    }

    public async Task SendBase64ToApi(string base64)
    {
        // creates a new request
        CloudVisionRequest request = new CloudVisionRequest(base64);
        // serializes it
        string serializedJson = request.Serialize();
        // sends it asynchronously

    }

    #endregion

    #region Commands Logic
    public void SubmitChanges()
    {
        // to do
        // write changes to config
        // make this window disappear and create the actual overlay
        ToggleOverlayVisibility();
        // invoking the event here
        OnSubmitClicked.Invoke();
        _configService.SaveConfig(this);
    }
    #endregion
    #region PropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)        // can be overridden
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
