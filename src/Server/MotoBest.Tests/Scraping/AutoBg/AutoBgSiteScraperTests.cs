using AngleSharp;

using MotoBest.Common;

using MotoBest.Services;
using MotoBest.Services.Scraping;
using MotoBest.Services.Scraping.Models;

using MotoBest.Tests.Mocks;
using MotoBest.Tests.Scraping.AutoBg;

using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

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
    [InlineData(nameof(SampleScrapedAdverts.Test_001))]
    [InlineData(nameof(SampleScrapedAdverts.Test_002))]
    [InlineData(nameof(SampleScrapedAdverts.Test_003))]
    [InlineData(nameof(SampleScrapedAdverts.Test_004))]
    [InlineData(nameof(SampleScrapedAdverts.Test_005))]
    [InlineData(nameof(SampleScrapedAdverts.Test_006))]
    [InlineData(nameof(SampleScrapedAdverts.Test_007))]
    [InlineData(nameof(SampleScrapedAdverts.Test_008))]
    [InlineData(nameof(SampleScrapedAdverts.Test_009))]
    [InlineData(nameof(SampleScrapedAdverts.Test_010))]
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult(string scrapedAdvertTestName)
    {
        var type = typeof(SampleScrapedAdverts);
        var field = type.GetField(scrapedAdvertTestName);

        string filePath = $"./Scraping/AutoBg/SampleScrapedAdvertPages/{scrapedAdvertTestName}.html";
        string html = await File.ReadAllTextAsync(filePath);

        var document = await browsingContext.OpenAsync(res => res.Content(html));
        var actualScrapedAdvert = new AutoBgSiteScraper(fakeDateTimeManager).ScrapeAdvert(document);

        var expectedScrapedAdvert = field?.GetValue(null) as ScrapedAdvert;
        expectedScrapedAdvert.AssertPropertyValues(actualScrapedAdvert);
    }

    [Theory]
    [InlineData(nameof(SampleSearchAdvertResults.Test_001))]
    [InlineData(nameof(SampleSearchAdvertResults.Test_002))]
    [InlineData(nameof(SampleSearchAdvertResults.Test_003))]
    public async Task ScrapeSearchAdvertResults_ShouldReturn_CorrectResult(string advertResultTestName)
    {
        var type = typeof(SampleSearchAdvertResults);
        var field = type.GetField(advertResultTestName);

        string filePath = $"./Scraping/AutoBg/SampleSearchAdvertResultPages/{advertResultTestName}.html";
        string html = await File.ReadAllTextAsync(filePath);

        var document = await browsingContext.OpenAsync(res => res.Content(html));

        var expectedAdvertResults = field?.GetValue(null) as SearchAdvertResult[];
        var actualAdvertResults = new AutoBgSiteScraper(fakeDateTimeManager).ScrapeSearchAdvertResults(document).ToArray();

        Assert.Equal(expectedAdvertResults, actualAdvertResults);
    }
}
