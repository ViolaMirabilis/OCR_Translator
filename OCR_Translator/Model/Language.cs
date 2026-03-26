using System.Dynamic;

namespace OCR_Translator.Model;

public class Language
{
    public string Name { get; set; } = string.Empty;
    // Initials have to be matched to the translation tool
    public string Initials { get; set; } = string.Empty;
}
