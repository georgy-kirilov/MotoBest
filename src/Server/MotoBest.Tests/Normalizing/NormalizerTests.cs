using MotoBest.Common;

using MotoBest.Services.Normalizing;
using MotoBest.Services.Scraping;
using MotoBest.Tests.Mocks;

using System.IO;
using System.Text.Json;

using Xunit;

namespace MotoBest.Tests.Normalizing;

public class NormalizerTests
{
    [Theory]
    [InlineData("Test-001")]
    [InlineData("Test-002")]
    [InlineData("Test-003")]
    [InlineData("Test-004")]
    [InlineData("Test-005")]
    [InlineData("Test-006")]
    [InlineData("Test-007")]
    [InlineData("Test-008")]
    [InlineData("Test-009")]
    [InlineData("Test-010")]
    public void Normalize_ShouldReturn_CorrectResult(string fileName)
    {
        string scrapedAdvertJson = File.ReadAllText($"./Scraping/AutoBg/ScrapedAdverts/{fileName}.json");
        string normalizedAdvertJson = File.ReadAllText($"./Normalizing/NormalizedAdverts/AutoBg/{fileName}.json");

        var scrapedAdvert = JsonSerializer.Deserialize<ScrapedAdvert>(scrapedAdvertJson);
        var expectedNormalizedAdvert = JsonSerializer.Deserialize<NormalizedAdvert>(normalizedAdvertJson);

        var normalizer = new Normalizer(new FakeCurrencyCourseProvider());
        var actualNormalizedAdvert = normalizer.Normalize(scrapedAdvert!);

        expectedNormalizedAdvert.AssertProperties(actualNormalizedAdvert);
    }
}
