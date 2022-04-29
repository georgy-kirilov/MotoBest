using AngleSharp;
using AngleSharp.Dom;

using System.IO;
using System.Linq;
using System.Threading.Tasks;

using MotoBest.Services.Common;
using MotoBest.Services.Scraping.Models;
using MotoBest.Services.Scraping.DesktopAutoBg;

using MotoBest.Tests.Mocks;

using Xunit;
using MotoBest.Common.Extensions;

namespace MotoBest.Tests.Scraping.DesktopAutoBg;

public class DesktopAutoBgSiteScraperTests
{
    private readonly IDateTimeManager fakeDateTimeManager;
    private readonly IBrowsingContext browsingContext;
    private readonly DesktopAutoBgSiteScraper siteScraper;

    public DesktopAutoBgSiteScraperTests()
    {
        fakeDateTimeManager = new FakeDateTimeManager();
        siteScraper = new DesktopAutoBgSiteScraper(fakeDateTimeManager);
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
    public async Task ScrapeAdvert_ShouldReturnCorrectResult(string scrapedAdvertTestName)
    {
        var field = typeof(DesktopAutoBgScrapedAdverts).GetField(scrapedAdvertTestName);
        var document = await OpenDocumentFromFileSystemAsync("ScrapedAdvertsPages", scrapedAdvertTestName);

        var actualScrapedAdvert = siteScraper.ScrapeAdvert(document);
        var expectedScrapedAdvert = field?.GetValue(null) as ScrapedAdvert;

        expectedScrapedAdvert.AssertProperties(actualScrapedAdvert);
    }

    [Theory]
    [InlineData(nameof(DesktopAutoBgSearchAdvertsResults.Test_001))]
    [InlineData(nameof(DesktopAutoBgSearchAdvertsResults.Test_002))]
    [InlineData(nameof(DesktopAutoBgSearchAdvertsResults.Test_003))]
    public async Task ScrapeSearchAdvertResults_ShouldReturnCorrectResult(string advertResultTestName)
    {
        var field = typeof(DesktopAutoBgSearchAdvertsResults).GetField(advertResultTestName);
        var document = await OpenDocumentFromFileSystemAsync("SearchAdvertsResultsPages", advertResultTestName);

        var expectedAdvertResults = field?.GetValue(null) as ScrapedSearchAdvertsResult[];
        var actualAdvertResults = siteScraper.ScrapeSearchAdvertsResults(document).ToArray();

        Assert.Equal(expectedAdvertResults, actualAdvertResults);
    }

    [Theory]
    [InlineData(nameof(DesktopAutoBgScrapedModels.Pobeda))]
    [InlineData(nameof(DesktopAutoBgScrapedModels.AlfaRomeo))]
    [InlineData(nameof(DesktopAutoBgScrapedModels.Honda))]
    [InlineData(nameof(DesktopAutoBgScrapedModels.Peugeot))]
    public async Task ScrapeAllModels_ShouldReturnCorrectResult(string scrapedModelsTestName)
    {
        var document = await OpenDocumentFromFileSystemAsync("ScrapedModelsPages", scrapedModelsTestName);

        var actualModels = siteScraper.ScrapeAllModels(document).ToArray();
        var expectedModels = typeof(DesktopAutoBgScrapedModels).GetField(scrapedModelsTestName)?.GetValue(null) as string[];

        Assert.Equal(expectedModels, actualModels);
    }

    private async Task<IDocument> OpenDocumentFromFileSystemAsync(string testPagesSubPath, string testCaseName)
    {
        string filePath = $"./Scraping/DesktopAutoBg/{testPagesSubPath}/{testCaseName}.html";
        string html = await File.ReadAllTextAsync(filePath);
        return await browsingContext.OpenAsync(res => res.Content(html));
    }
}
