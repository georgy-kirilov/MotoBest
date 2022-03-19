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
    [InlineData("19495548")]
    [InlineData("94058504")]
    [InlineData("17067064")]
    [InlineData("78524242")]
    [InlineData("82790797")]
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult(string smapleAdvertRemoteId)
    {
        using FileStream openStream = File.OpenRead($"./AutoBg/ScrapeModels/{smapleAdvertRemoteId}.json");
        var expectedScrapeModel = await JsonSerializer.DeserializeAsync<AdvertScrapeModel>(openStream);

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        var scraper = new AutoBgScraper();

        string html = await File.ReadAllTextAsync($"./AutoBg/SampleAdverts/{smapleAdvertRemoteId}.html");

        var document = await context.OpenAsync(res => res.Content(html));
        var actualScrapeModel = scraper.ScrapeAdvert(document);

        var properties = typeof(AdvertScrapeModel).GetProperties();

        foreach (var property in properties)
        {
            var expected = property.GetValue(expectedScrapeModel);
            var actual = property.GetValue(actualScrapeModel);

            Assert.Equal(expected, actual);
        }
    }
}
