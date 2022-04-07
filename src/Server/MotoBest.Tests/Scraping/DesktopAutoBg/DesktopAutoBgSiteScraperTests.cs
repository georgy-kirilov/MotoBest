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

public class DesktopAutoBgSiteScraperTests
{
    private readonly IDateTimeManager fakeDateTimeManager;
    private readonly IBrowsingContext browsingContext;

    public DesktopAutoBgSiteScraperTests()
    {
        fakeDateTimeManager = new FakeDateTimeManager();
        browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    }

    [Theory]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_001))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_002))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_003))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_004))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_005))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_006))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_007))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_008))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_009))]
    [InlineData(nameof(DesktopAutoBgScrapedAdverts.Test_010))]
    public async Task ScrapeAdvert_ShouldReturn_CorrectResult(string scrapedAdvertTestName)
    {
        var type = typeof(DesktopAutoBgScrapedAdverts);
        var field = type.GetField(scrapedAdvertTestName);

        string filePath = $"./Scraping/DesktopAutoBg/ScrapedAdvertPages/{scrapedAdvertTestName}.html";
        string html = await File.ReadAllTextAsync(filePath);

        var document = await browsingContext.OpenAsync(res => res.Content(html));
        var actualScrapedAdvert = new DesktopAutoBgSiteScraper(fakeDateTimeManager).ScrapeAdvert(document);

        var expectedScrapedAdvert = field?.GetValue(null) as ScrapedAdvert;
        expectedScrapedAdvert.AssertPropertyValues(actualScrapedAdvert);
    }

    [Theory]
    [InlineData(nameof(DesktopAutoBgSearchAdvertResults.Test_001))]
    [InlineData(nameof(DesktopAutoBgSearchAdvertResults.Test_002))]
    [InlineData(nameof(DesktopAutoBgSearchAdvertResults.Test_003))]
    public async Task ScrapeSearchAdvertResults_ShouldReturn_CorrectResult(string advertResultTestName)
    {
        var type = typeof(DesktopAutoBgSearchAdvertResults);
        var field = type.GetField(advertResultTestName);

        string filePath = $"./Scraping/DesktopAutoBg/SearchAdvertResultPages/{advertResultTestName}.html";
        string html = await File.ReadAllTextAsync(filePath);

        var document = await browsingContext.OpenAsync(res => res.Content(html));

        var expectedAdvertResults = field?.GetValue(null) as SearchAdvertResult[];
        var actualAdvertResults = new DesktopAutoBgSiteScraper(fakeDateTimeManager).ScrapeSearchAdvertResults(document).ToArray();

        Assert.Equal(expectedAdvertResults, actualAdvertResults);
    }
}
