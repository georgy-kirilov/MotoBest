using System.Text;

namespace MotoBest.Common.Extensions;

public static class StringExtensions
{
    public static string RemoveRepeatingWhiteSpaces(this string text)
    {
        var stringBuilder = new StringBuilder();

        foreach (string word in text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            stringBuilder.Append(word);
            stringBuilder.Append(' ');
        }

        return stringBuilder.ToString().Trim();
    }

    public static string RemoveStrings(this string text, params string[] stringsToSanitize)
        => text.ReplaceMany(newValue: string.Empty, stringsToSanitize);


    public static string ReplaceMany(this string text, string newValue, params string[] oldValues)
    {
        var stringBuilder = new StringBuilder(text);

        foreach (string oldValue in oldValues)
        {
            stringBuilder.Replace(oldValue, newValue);
        }

        return stringBuilder.ToString();
    }
}
