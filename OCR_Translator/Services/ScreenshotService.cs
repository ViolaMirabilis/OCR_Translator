using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OCR_Translator.Services;

public class ScreenshotService
{
    // ../../debug
    private string workingDirectory = Environment.CurrentDirectory;
    

    public void TakeScreenshot(int width, int height)
    {
        try
        {
         
            using var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                // the first (0,0) is top left of the screen
                // the 2nd (0,0) is the top left of the bitmap
                // the width and height is divided, so the middle (x,y) of the provided resolution are passed
                // i.e. the screenshot is taken from the middle of the desktop, instead of top left (x,y)
                g.CopyFromScreen(width/2, height/2, 0, 0, bitmap.Size);
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                bitmap.Save($"{projectDirectory}\\tmpscreenshot.png", ImageFormat.Png);

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }


    public string ConvertBitmapToBase64(Bitmap screenshot)
    {
        return "tmp";
    }

}
