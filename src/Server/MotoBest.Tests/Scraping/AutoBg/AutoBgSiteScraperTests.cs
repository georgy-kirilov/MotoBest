using AngleSharp;

using MotoBest.Common;
using MotoBest.Services;
using MotoBest.Services.Scraping;
using MotoBest.Services.Scraping.Models;
using MotoBest.Tests.Mocks;
using MotoBest.Tests.Scraping;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

using static MotoBest.Tests.Scraping.SampleScrapedAdverts.AutoBg;

namespace MotoBest.Tests.AutoBg;

public class AutoBgSiteScraperTests
{
    private readonly IDateTimeManager fakeDateTimeManager;
    private readonly IBrowsingContext browsingContext;

    public AutoBgSiteScraperTests()
    {
        fakeDateTimeManager = new FakeDateTimeManager();
        browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    }

    [Theory]
    //[InlineData("Test-001")]
    //[InlineData("Test-002")]
    //[InlineData("Test-003")]
    //[InlineData("Test-004")]
    //[InlineData("Test-005")]
    //[InlineData("Test-006")]
    //[InlineData("Test-007")]
    //[InlineData("Test-008")]
    //[InlineData("Test-009")]
    //[InlineData("Test-010")]
    [InlineData(nameof(Test_001))]
    [InlineData(nameof(Test_002))]
    [InlineData(nameof(Test_003))]
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult(string scrapedAdvertName)
    {
        var type = typeof(SampleScrapedAdverts.AutoBg);
        var field = type.GetField(scrapedAdvertName);

        string filePath = $"./Scraping/AutoBg/SampleAdverts/{scrapedAdvertName}.html";
        string html = await File.ReadAllTextAsync(filePath);

        var document = await browsingContext.OpenAsync(res => res.Content(html));
        var actualScrapedAdvert = new AutoBgSiteScraper(fakeDateTimeManager).ScrapeAdvert(document);

        var expectedScrapedAdvert = field?.GetValue(null) as ScrapedAdvert;
        expectedScrapedAdvert.AssertPropertyValues(actualScrapedAdvert);
    }

    [Theory]
    [InlineData("Test-001")]
    [InlineData("Test-002")]
    [InlineData("Test-003")]
    public async Task ScrapeAdvertResultsFromPage_ShouldReturn_CorrectResult(string sampleAdvertResultPageFileName)
    {
        using FileStream openStream = File.OpenRead(
            $"./Scraping/AutoBg/AdvertResults/{sampleAdvertResultPageFileName}.json");

        var expectedAdvertResults = await JsonSerializer.DeserializeAsync<SearchAdvertResult[]>(openStream);

        var scraper = new AutoBgSiteScraper(fakeDateTimeManager);

        string html = await File.ReadAllTextAsync(
            $"./Scraping/AutoBg/SampleAdvertResultPages/{sampleAdvertResultPageFileName}.html");

        var document = await browsingContext.OpenAsync(res => res.Content(html));

        var actualAdvertResults = scraper.ScrapeSearchAdvertResults(document).ToArray();

        Assert.Equal(expectedAdvertResults, actualAdvertResults);
    }
}
