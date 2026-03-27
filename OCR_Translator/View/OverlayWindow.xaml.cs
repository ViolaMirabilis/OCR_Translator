using OCR_Translator.Interfaces;
using OCR_Translator.Model;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;


namespace OCR_Translator.View;


public partial class OverlayWindow : Window
{

    public int TextBoxFontSize { get; set; }
    public string TextBoxColor { get; set; }
    public string TextColor { get; set; }
    public int GameWidth { get; set; }
    public int GameHeight { get; set; }
    public List<OverlayTextbox> OverlayTextboxCollection { get; private set; } = new List<OverlayTextbox>();
    public OverlayWindow(int fontSize, string tbColor, string txtColor, int width, int height, List<OverlayTextbox> overlayTextboxCollection)
    {
        InitializeComponent();
        TextBoxFontSize = fontSize;
        TextBoxColor = tbColor;
        TextColor = txtColor;
        GameWidth = width;
        GameHeight = height;
        OverlayTextboxCollection = overlayTextboxCollection;

        
        InitializeSettingsFromConfig();
        PutAllTextboxesOnCanvas();
    }

    private void InitializeSettingsFromConfig()
    {
        // to do
        Overlay.Height = GameHeight;
        Overlay.Width = GameWidth;
        OverlayCanvas.Height = GameHeight;
        OverlayCanvas.Width = GameWidth;
        DataContext = this;
    }

    private void PutAllTextboxesOnCanvas()
    {
        foreach (var sentence in OverlayTextboxCollection)
        {
            PutTextboxOnCanvas(sentence);
        }
    }
    private void PutTextboxOnCanvas(OverlayTextbox textbox)
    {
        Border border = new Border { Height = 14, Width = textbox.Text.Length * 7, CornerRadius = new CornerRadius(5), Background = Brushes.White, Opacity = 0.95 };
        // textblock that will be inside the border
        TextBlock textblock = new TextBlock { Foreground = Brushes.Black, FontSize = 10, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Text = textbox.Text };
        //border with textblock
        border.Child = textblock;
        // setting up canvas location
        Canvas.SetTop(border, textbox.Y);
        Canvas.SetLeft(border, textbox.X);
        OverlayCanvas.Children.Add(border);
    }

}
