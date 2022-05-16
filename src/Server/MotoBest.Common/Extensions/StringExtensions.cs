using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MotoBest.Common.Extensions;

public static class StringExtensions
{
    public static T? ParseJsonTo<T>(this string jsonContent)
        => JsonSerializer.Deserialize<T>(jsonContent, GlobalConstants.BasicJsonOptions);

    public static string RemoveRepeatingWhiteSpaces(this string text)
    {
        var stringBuilder = new StringBuilder();
        var words = text.Split(new[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            stringBuilder.Append(word);
            stringBuilder.Append(' ');
        }

        return stringBuilder.ToString().Trim();
    }

    public static string ReplaceFirst(this string text, string oldValue, string newValue)
        => new Regex(Regex.Escape(oldValue)).Replace(text, newValue, 1);

    public static string RemoveStrings(this string text, params string[] stringsToSanitize)
        => text.ReplaceWith(newValue: string.Empty, stringsToSanitize);


    public static string ReplaceWith(this string text, string newValue, params string[] oldValues)
    {
        var stringBuilder = new StringBuilder(text);

        foreach (string oldValue in oldValues)
        {
            stringBuilder.Replace(oldValue, newValue);
        }

        return stringBuilder.ToString();
    }
}
