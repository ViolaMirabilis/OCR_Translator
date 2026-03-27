using Newtonsoft.Json;
using OCR_Translator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace OCR_Translator.Services;

public class TranslationService
{
    public ObservableCollection<Language> InitialiseLanguageCollection()
    {
        return new ObservableCollection<Language> {
            new Language { Name = "English", Initials = "En-us" },
            new Language { Name = "Thai", Initials = "Th-th" },
            new Language { Name = "Test", Initials = "Test" },
            new Language { Name = "Korean", Initials = "Kr-kr" } };
    }

    public void DeserializeCloudVisionResponse(string response)
    {
        // Initializing two stringbuilders
        // one for words, one for sentences
        StringBuilder wordSB = new StringBuilder();
        StringBuilder sentenceSB = new StringBuilder();
        // a list which contains the original sentences with corresponding (x,y) coordinates
        List<OverlayTextbox> textboxlist = new List<OverlayTextbox>();


        // deserializes the result from the google API 
        var deserialized = JsonConvert.DeserializeObject<CloudVisionResponse.Rootobject>(response);
    }
}
