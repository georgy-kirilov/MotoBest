using AngleSharp;
using MotoBest.Services.Scraping;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MotoBest.Tests.AutoBg;

public class AutoBgScraperTests
{
    [Fact]
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult()
    {
        using FileStream openStream = File.OpenRead("./AutoBg/ScrapeModels/auto-bg-scrape-models.json");
        var expectedScrapeModels = await JsonSerializer.DeserializeAsync<AdvertScrapeModel[]>(openStream);

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        var scraper = new AutoBgScraper();

        foreach (var expectedScrapeModel in expectedScrapeModels!)
        {
            string path = $"./AutoBg/SampleAdverts/{expectedScrapeModel.RemoteId}.html";
            string html = await File.ReadAllTextAsync(path);

            var document = await context.OpenAsync(res => res.Content(html));
            var actualScrapeModel = scraper.ScrapeAdvert(document);

            var assertions = new Assertion<string, string>[]
            {
                new(expectedScrapeModel.RemoteId, actualScrapeModel.RemoteId),
                new(expectedScrapeModel.Title, actualScrapeModel.Title),
                new(expectedScrapeModel.Description, actualScrapeModel.Description),
            };

            foreach (var assertion in assertions)
            {
                Assert.Equal(assertion.Expected, assertion.Actual);
            }
        }
    }
}

public class Assertion<TExpected, TActual>
{
    public Assertion(TExpected? expected, TActual? actual)
    {
        Expected = expected;
        Actual = actual;
    }

    public TExpected? Expected { get; init; }

    public TActual? Actual { get; init; }
}
