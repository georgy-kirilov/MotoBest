using AngleSharp;

using MotoBest.Services.Scraping;

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace MotoBest.Tests.AutoBg;

public class AutoBgScraperTests
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
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult(string sampleAdvertFileName)
    {
        using FileStream openStream = File.OpenRead($"./AutoBg/ScrapedAdverts/{sampleAdvertFileName}.json");
        var expectedScrapedAdvert = await JsonSerializer.DeserializeAsync<ScrapedAdvert>(openStream);

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        var scraper = new AutoBgScraper();

        string html = await File.ReadAllTextAsync($"./AutoBg/SampleAdverts/{sampleAdvertFileName}.html");

        var document = await context.OpenAsync(res => res.Content(html));
        var actualScrapedAdvert = scraper.ScrapeAdvert(document);

        var properties = typeof(ScrapedAdvert).GetProperties();

        foreach (var property in properties)
        {
            var expectedValue = property.GetValue(expectedScrapedAdvert);
            var actualValue = property.GetValue(actualScrapedAdvert);

            Assert.Equal(expectedValue, actualValue);
        }
    }
}
