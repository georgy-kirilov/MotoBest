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
    [InlineData("Test-1")]
    [InlineData("Test-2")]
    [InlineData("Test-3")]
    [InlineData("Test-4")]
    [InlineData("Test-5")]
    [InlineData("Test-6")]
    [InlineData("Test-7")]
    [InlineData("Test-8")]
    [InlineData("Test-9")]
    [InlineData("Test-10")]
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
