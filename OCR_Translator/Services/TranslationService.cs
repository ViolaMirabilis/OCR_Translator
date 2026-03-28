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
    // a list which contains the original sentences with corresponding (x,y) coordinates
    public List<OverlayTextbox> textboxlist { get; private set; } = new List<OverlayTextbox>();
    public ObservableCollection<Language> InitialiseLanguageCollection()
    {
        return new ObservableCollection<Language> {
            new Language { Name = "Auto detect", Initials = "auto" },
            new Language { Name = "English", Initials = "en" },
            new Language { Name = "Thai", Initials = "th" },
            new Language { Name = "Korean", Initials = "ko" } };
    }

    // takes in the Cloud Vision API as a response
    public void DeserializeCloudVisionResponse(string serializedResponse)
    {
        // Initializing two stringbuilders
        // one for words, one for sentences
        StringBuilder wordSB = new StringBuilder();
        StringBuilder sentenceSB = new StringBuilder();

        // deserializes the result from the google API 
        var deserialized = JsonConvert.DeserializeObject<CloudVisionResponse.Rootobject>(serializedResponse);

        // gets the first page
        if (deserialized == null)
        {
            Console.WriteLine("The file is empty");
            return;
        }
        var page = deserialized.responses[0].fullTextAnnotation.pages[0];
        foreach (var block in page.blocks)
        {
            // block has paragraphs 
            foreach (var paragraph in block.paragraphs)
            {
                // paragraphs have words
                // getting the top left vertices (x,y) from a given paragraph
                var topX = paragraph.boundingBox.vertices[0].x;
                var topY = paragraph.boundingBox.vertices[0].y;
                Console.WriteLine($"Bounding box: Top XY ({topX},{topY})");

                foreach (var word in paragraph.words)
                {
                    // and symbols (LETTERS)
                    foreach (var symbol in word.symbols)
                    {
                        // using sb instead of concatenation
                        // forming one full word from singular characters (symbols)
                        wordSB.Append(symbol.text);
                    }
                    // testing purposes and visualisation if it works as intended
                    Console.WriteLine($"{wordSB.ToString()} -> word formed from letters");

                    // appending the sentence with a singular word
                    sentenceSB.Append(wordSB);

                    // clearing out both the word string builder, so another word can be created
                    wordSB.Clear();
                }

                // words in the paragraph finished? Add it as one "text"
                // adding an entire sentence to the list
                textboxlist.Add(new OverlayTextbox { Text = sentenceSB.ToString(), X = topX, Y = topY });
                // clearing out the sentence, so another one can be created
                sentenceSB.Clear();
            }
        }
    }

    /// <summary>
    /// Creates a summary of all the sentences, separated by "@#$"
    /// Used to pass into the translation library
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public string CombineIntoOneString(List<OverlayTextbox> list)
    {
        StringBuilder allLinesCombined = new StringBuilder();

        foreach (var word in list)
        {
            // appends the SB with the word and the "Separator"
            allLinesCombined.Append($"{word.Text}\n");
        }

        WriteToFile(allLinesCombined.ToString());
        return allLinesCombined.ToString();
    }

    public void WriteToFile(string stringsCombined)
    {
        string path = @"C:\Users\zajac\Desktop\dump.txt";
        System.IO.File.WriteAllText(path, stringsCombined);
    }

    public void ReplaceOriginalTextWithTranslation(List<OverlayTextbox> textboxlist, string allLinesCombined)
    {
        string[] sentences = allLinesCombined.Split("\\n");
        int index = 0;
        try
        {
            foreach (var word in textboxlist)
            {
                // replaces the text and increases the index
                word.Text = sentences[index++];
            }
        }
        catch
        {

        }
    }
}
