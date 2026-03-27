using System.Text.Json;

namespace OCR_Translator.Model;

// @see docs: https://docs.cloud.google.com/vision/docs/ocr?_gl=1*15ga719*_up*MQ..&gclid=Cj0KCQjw1ZjOBhCmARIsADDuFTDzQ1O_HxbAiRxdY9YuhMWkUZowWo-7HHQxOG0icdwTxDR5OZe6WyQaAu9YEALw_wcB&gclsrc=aw.ds

// API request structure
public class CloudVisionRequest
{
    public string Base64 { get; set; } = string.Empty;
    public CloudVisionRequest(string base64)
    {
        Base64 = base64;
    }
    public class Rootobject
    {
        public Request[] requests { get; set; }
    }

    public class Request
    {
        public Image image { get; set; }
        public Feature[] features { get; set; }
    }

    public class Image
    {
        // CONTENT is needed instead of "source", because of base64string
        public string content { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
    }

    public string Serialize()
    {
        // going bottom up
        Feature feature = new Feature{ type = "TEXT_DOCUMENT_DETECTION"};
        // sets teh content to base64 (screenshot in bytes, converter to base64)
        Image image = new Image { content = Base64 };
        // creates a new array of "features", including the "feature" property
        Request request = new Request { image = image, features = new Feature[] { feature } };
        // same as above
        Request[] requests = new Request[] { request };
        // assigning the Request[] array to root object's one.
        Rootobject root = new Rootobject { requests = requests };

        string serialized = JsonSerializer.Serialize(root);
        return serialized;

    }

}



