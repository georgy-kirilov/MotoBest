using System.Text;

namespace MotoBest.Common;

public static class StringExtensions
{
    public static string RemoveRepeatingWhiteSpaces(this string text)
    {
        var descriptionBuilder = new StringBuilder();

        foreach (string word in text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            descriptionBuilder.Append(word);
            descriptionBuilder.Append(' ');
        }

        return descriptionBuilder.ToString().Trim();
    }

    public static string RemoveStrings(this string text, params string[] stringsToSanitize)
        => text.ReplaceManyWith(newValue: string.Empty, stringsToSanitize);


    public static string ReplaceManyWith(this string text, string newValue, params string[] oldValues)
    {
        var stringBuilder = new StringBuilder(text);

        foreach (string oldValue in oldValues)
        {
            stringBuilder.Replace(oldValue, newValue);
        }

        return stringBuilder.ToString();
    }
}
