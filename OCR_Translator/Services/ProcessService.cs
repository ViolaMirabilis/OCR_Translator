using System.Diagnostics;

namespace OCR_Translator.Services;
/// <summary>
/// Starts a cmd.exe process and runs the LibreTranslation launch script.
/// By default runs on localhost:5000
/// </summary>
public class ProcessService
{
    public void StartLibreTranslationProcess(string sourceLanguage, string targetLanguage)
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", $@"/clibretranslate --load-only {sourceLanguage},{targetLanguage}");
        // console isn't visible once launched
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.Verb = "runas";
        process.StartInfo = startInfo;
        process.Start();
    }
}
