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
    public OverlayWindow(int fontSize, string tbColor, string txtColor, int width, int height)
    {
        InitializeComponent();
        TextBoxFontSize = fontSize;
        TextBoxColor = tbColor;
        TextColor = txtColor;
        GameWidth = width;
        GameHeight = height;
        
        InitializeSettingsFromConfig();
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

    public void PutAllTextboxesOnCanvas(List<OverlayTextbox> overlayTextboxCollection)
    {
        foreach (var sentence in overlayTextboxCollection)
        {
            PutTextboxOnCanvas(sentence);
        }
    }
    private void PutTextboxOnCanvas(OverlayTextbox textbox)
    {
        Border border = new Border {Padding = new Thickness(1), MaxWidth=250, MaxHeight=150, CornerRadius = new CornerRadius(5), Background = Brushes.White, Opacity = 0.95};
        // textblock that will be inside the border
        TextBlock textblock = new TextBlock { Foreground = Brushes.Black, FontSize = 10,  HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Text = textbox.Text, MaxWidth=250, TextWrapping=TextWrapping.Wrap, Margin = new Thickness(1)};
        //border with textblock
        border.Child = textblock;
        // setting up canvas location
        Canvas.SetTop(border, textbox.Y);
        Canvas.SetLeft(border, textbox.X);
        OverlayCanvas.Children.Add(border);
    }

}
