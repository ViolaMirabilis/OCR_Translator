namespace OCR_Translator.Model;

// @see docs: https://docs.cloud.google.com/vision/docs/ocr?_gl=1*15ga719*_up*MQ..&gclid=Cj0KCQjw1ZjOBhCmARIsADDuFTDzQ1O_HxbAiRxdY9YuhMWkUZowWo-7HHQxOG0icdwTxDR5OZe6WyQaAu9YEALw_wcB&gclsrc=aw.ds

// API request structure
public class CloudisionRequest
{
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

}



