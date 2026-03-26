using OCR_Translator.Model;
using OCR_Translator.Services;
using OCR_Translator.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static OCR_Translator.NativeWindowsHooks;
using OCR_Translator.Interfaces;
using OCR_Translator.View;
using System.Configuration;
using OCR_Translator.Config;


namespace OCR_Translator
{
    public partial class MainWindow : Window, IOverlaySettings
    {
        #region Configuration
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        #endregion
        #region Variables for the NativeWindowsHooks
        // responsible for registering KEY DOWN event
        int WM_KEYDOWN = 0x0100;
        // defined key responsible for opening up the overlay (pg up in this case)
        int VK_Prior = 0x21;
        // WH_KEYBOARD_LL needed for LowLeveleyboardProc and SetWindowsHookEx
        private const int WH_KEYBOARD_LL = 13;

        IntPtr windowHandle;
        static IntPtr hHook = 0;
        // the key down basically
        HookProc KeyboardHookProcedure;

        #endregion

        #region Services
        private readonly TranslationService _translationService = new TranslationService();
        private OverlayWindow _overlayWindow;
        private readonly ConfigService _configService = new ConfigService();
        #endregion
        #region Properties and fields
        // overlay visibility variable
        private bool _isVisible = false;

        // holds initials for the languages (FROM - TO)
        private string _translateFrom = string.Empty;
        public string TranslateFrom
        {
            get => _translateFrom;
            set { _translateFrom = value; OnPropertyChanged(); }
        }
        private string _translateTo = string.Empty;
        public string TranslateTo
        {
            get => _translateTo;
            set { _translateTo = value; OnPropertyChanged(); }
        }
        // bindable list of supported translation languages
        public ObservableCollection<Language> SupportedTranslationLanguages { get; set; }
        
        //Bindable font size
        private int _textBoxFontSize = 10;
        public int TextBoxFontSize { get => _textBoxFontSize; set { _textBoxFontSize = value; OnPropertyChanged(); } }

        // Bindable HEX for the textbox color
        private string _textBoxColor = "#FFFFFF";
        public string TextBoxColor { get => _textBoxColor; set { _textBoxColor = value; OnPropertyChanged();  } }

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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            // @see: https://stackoverflow.com/questions/1556182/finding-the-handle-to-a-wpf-window
            // gets the current window handle (the main, transparent window)
            windowHandle = new WindowInteropHelper(this).Handle;
            // hooking into the current window
            InitialiseWindowHook(windowHandle);
            SupportedTranslationLanguages = _translationService.InitialiseLanguageCollection();

            SubmitConfigChanges = new RelayCommand(_ => SubmitChanges(), _ => true);


            // Initialising the config file
            _configService.LoadConfig(this);
            
        }

        #region Methods related to hooking into the window
        // @see: https://www.betaarchive.com/wiki/index.php/Microsoft_KB_Archive/318804
        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            else
            {
                // do some code
                if (wParam == WM_KEYDOWN)
                {
                    // holds the VK value of the currently pressed key.
                    // this helped a lot: https://www.reddit.com/r/csharp/comments/876cq6/requesting_help_trying_to_understand_something/
                    int pressedKeyVK = Marshal.ReadInt32(lParam);

                    // if currently pressed key is equal to the key defined by us (page up in this case)
                    if (pressedKeyVK == VK_Prior)
                    {
                        //MessageBox.Show("THE BUTTON WORKS!");
                        ToggleOverlayVisibility();
                    }
                }
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
        }
        private void InitialiseWindowHook(IntPtr windowHandle)
        {
            if (hHook == 0)
            {
                // passing in the delegate
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                // passing in the "current thread" as 0, so it listens for input globally
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, 0, 0);
                // the value of hHook should be changed now
                if (hHook == 0)
                {
                    MessageBox.Show("SetWindowsHookEx failed");
                    return;
                }
            }
        }
        #endregion
        #region First launch window settings
        private void ToggleOverlayVisibility()
        {
            if (!_isVisible)
            {
                // if overlay is not visible, it creates a new object once and assigns it to the private variable
                _overlayWindow = new OverlayWindow(TextBoxFontSize, TextBoxColor, TextColor, GameWidth, GameHeight);
                _overlayWindow.Show();
                
                _isVisible = true;
            }
            else
            {
                // it means the overlay window already exists, so it just toggles the view.
                _isVisible = false;
                _overlayWindow.Hide();
            }

        }

        #endregion

        #region Commands Logic
        public void SubmitChanges()
        {
            // to do
            // write changes to config
            // make this window disappear and create the actual overlay
            StartupWindow.Hide();
            ToggleOverlayVisibility();

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
}