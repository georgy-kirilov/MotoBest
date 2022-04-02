using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using Xunit;

namespace MotoBest.Common;

public static class ObjectExtensions
{
    public static string ToJson(this object obj)
        => JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        });

    public static void AssertProperties<T>(this T expected, T actual)
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var expectedValue = property.GetValue(expected);
            var actualValue = property.GetValue(actual);

            Assert.Equal(expectedValue, actualValue);
        }
    }
}
