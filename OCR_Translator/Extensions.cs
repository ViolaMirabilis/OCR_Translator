namespace OCR_Translator;

public static class Extensions
{
    public static string ConvertToHex(this string str)
    {
        if (!str.StartsWith('#') && str.Length == 6)
        {
            return "#" + str;
        }
        return str;
    }

    public static bool IsHex(this string str)
    {
        if (!str.StartsWith('#'))
        {
            // if doesnt start with # and is longer than 6, false
            if (str.Length > 6)
                return false;
        }
        // if starts with # but longer than 7, false
        else if (str.Length > 7)
            return false;

        return true;
    }
}
