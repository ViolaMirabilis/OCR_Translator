namespace OCR_Translator.Model;

/// <summary>
/// Parsed from Google API response
/// </summary>
public class CloudVisionResponse
{
    public class Rootobject
    {
        public Response[] responses { get; set; }
    }

    public class Response
    {
        public Textannotation[] textAnnotations { get; set; }
        public Fulltextannotation fullTextAnnotation { get; set; }
    }

    public class Fulltextannotation
    {
        public Page[] pages { get; set; }
        public string text { get; set; }
    }

    public class Page
    {
        public Property1 property { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Block[] blocks { get; set; }
        public float confidence { get; set; }
    }

    public class Property1
    {
        public Detectedlanguage[] detectedLanguages { get; set; }
    }

    public class Detectedlanguage
    {
        public string languageCode { get; set; }
        public float confidence { get; set; }
    }

    public class Block
    {
        public Boundingbox boundingBox { get; set; }
        public Paragraph[] paragraphs { get; set; }
        public string blockType { get; set; }
        public float confidence { get; set; }
    }

    public class Boundingbox
    {
        public Vertex[] vertices { get; set; }
    }

    public class Vertex
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Paragraph
    {
        public Boundingbox1 boundingBox { get; set; }
        public Word[] words { get; set; }
        public float confidence { get; set; }
    }

    public class Boundingbox1
    {
        public Vertex1[] vertices { get; set; }
    }

    public class Vertex1
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Word
    {
        public Property2 property { get; set; }
        public Boundingbox2 boundingBox { get; set; }
        public Symbol[] symbols { get; set; }
        public float confidence { get; set; }
    }

    public class Property2
    {
        public Detectedlanguage1[] detectedLanguages { get; set; }
    }

    public class Detectedlanguage1
    {
        public string languageCode { get; set; }
        public int confidence { get; set; }
    }

    public class Boundingbox2
    {
        public Vertex2[] vertices { get; set; }
    }

    public class Vertex2
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Symbol
    {
        public Boundingbox3 boundingBox { get; set; }
        public string text { get; set; }
        public float confidence { get; set; }
        public Property3 property { get; set; }
    }

    public class Boundingbox3
    {
        public Vertex3[] vertices { get; set; }
    }

    public class Vertex3
    {
        public int y { get; set; }
        public int x { get; set; }
    }

    public class Property3
    {
        public Detectedbreak detectedBreak { get; set; }
    }

    public class Detectedbreak
    {
        public string type { get; set; }
    }

    public class Textannotation
    {
        public string locale { get; set; }
        public string description { get; set; }
        public Boundingpoly boundingPoly { get; set; }
    }

    public class Boundingpoly
    {
        public Vertex4[] vertices { get; set; }
    }

    public class Vertex4
    {
        public int y { get; set; }
        public int x { get; set; }
    }
}
