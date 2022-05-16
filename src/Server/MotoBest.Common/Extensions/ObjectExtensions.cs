using System.Text.Json;

using Xunit;

namespace MotoBest.Common.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString(this object obj)
        => JsonSerializer.Serialize(obj, GlobalConstants.BasicJsonOptions);

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
