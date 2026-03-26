using OCR_Translator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
}
