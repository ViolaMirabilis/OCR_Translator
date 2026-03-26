using OCR_Translator.Interfaces;
using System.Windows;

namespace OCR_Translator.View;


public partial class OverlayWindow : Window
{
    public int TextBoxFontSize { get; set; }
    public string TextBoxColor { get; set; }
    public string TextColor { get; set; }
    public int GameWidth { get; set; }
    public int GameHeight { get; set; }
    public OverlayWindow(int fontSize, string tbColor, string txtColor, int width, int height)
    {
        TextBoxFontSize = fontSize;
        TextBoxColor = tbColor;
        TextColor = txtColor;
        GameWidth = width;
        GameHeight = height;

        InitializeComponent();
        InitializeSettingsFromConfig();

    }

    private void InitializeSettingsFromConfig()
    {
        // to do
        Overlay.Height = GameHeight;
        Overlay.Width = GameWidth;
    }

}
