using AngleSharp;

using MotoBest.Common;
using MotoBest.Services;
using MotoBest.Services.Scraping;
using MotoBest.Tests.Mocks;

using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace MotoBest.Tests.AutoBg;

public class AutoBgScraperTests
{
    private readonly IBrowsingContext browsingContext;
    private readonly IDateTimeManager fakeDateTimeManager;

    public AutoBgScraperTests()
    {
        browsingContext = BrowsingContext.New(
            Configuration.Default.WithDefaultLoader());

        fakeDateTimeManager = new FakeDateTimeManager();
    }

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
        using FileStream openStream = File.OpenRead(
            $"./Scraping/AutoBg/ScrapedAdverts/{sampleAdvertFileName}.json");

        var expectedScrapedAdvert = await JsonSerializer.DeserializeAsync<ScrapedAdvert>(openStream);

        var scraper = new AutoBgScraper(fakeDateTimeManager);

        string html = await File.ReadAllTextAsync(
            $"./Scraping/AutoBg/SampleAdverts/{sampleAdvertFileName}.html");

        var document = await browsingContext.OpenAsync(res => res.Content(html));
        var actualScrapedAdvert = scraper.ScrapeAdvert(document);

        expectedScrapedAdvert.AssertProperties(actualScrapedAdvert);
    }

    [Theory]
    [InlineData("Test-001")]
    [InlineData("Test-002")]
    [InlineData("Test-003")]
    public async Task ScrapeAdvertResultsFromPage_ShouldReturn_CorrectResult(string sampleAdvertResultPageFileName)
    {
        using FileStream openStream = File.OpenRead(
            $"./Scraping/AutoBg/AdvertResults/{sampleAdvertResultPageFileName}.json");

        var expectedAdvertResults = await JsonSerializer.DeserializeAsync<AdvertResult[]>(openStream);

        var scraper = new AutoBgScraper(fakeDateTimeManager);

        string html = await File.ReadAllTextAsync(
            $"./Scraping/AutoBg/SampleAdvertResultPages/{sampleAdvertResultPageFileName}.html");

        var document = await browsingContext.OpenAsync(res => res.Content(html));

        var actualAdvertResults = scraper.ScrapeAdvertResultsFromPage(document).ToArray();

        Assert.Equal(expectedAdvertResults, actualAdvertResults);
    }
}
