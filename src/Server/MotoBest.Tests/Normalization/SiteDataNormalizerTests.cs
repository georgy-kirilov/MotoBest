using Xunit;

using MotoBest.Common.Extensions;

using MotoBest.Services.Common.Units;
using MotoBest.Services.Normalization;
using MotoBest.Services.Scraping.Models;

using MotoBest.Tests.Mocks;
using MotoBest.Tests.Scraping.DesktopAutoBg;

using static MotoBest.Tests.Normalizing.DesktopAutoBgNormalizedAdverts;

namespace MotoBest.Tests.Normalizing;

public class SiteDataNormalizerTests
{
    private readonly IUnitsManager fakeUnitsManager;

    public SiteDataNormalizerTests()
    {
        fakeUnitsManager = new FakeUnitsManager();
    }

    [Theory]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_001))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_002))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_003))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_004))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_005))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_006))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_007))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_008))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_009))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(Test_010))]
    public void NormalizeAdvert_ShouldReturn_CorrectResult(Type normalizedAdvertSource, Type scrapedAdvertSource, string normalizedAdvertTestName)
    {
        var expectedNormalizedAdvert = normalizedAdvertSource
            .GetField(normalizedAdvertTestName)?
            .GetValue(null) as NormalizedAdvert;

        var scrapedAdvert = scrapedAdvertSource
            .GetField(normalizedAdvertTestName)?
            .GetValue(null) as ScrapedAdvert;

        var actualNormalizedAdvert = new SiteDataNormalizer(fakeUnitsManager).NormalizeAdvert(scrapedAdvert!);
        expectedNormalizedAdvert.AssertProperties(actualNormalizedAdvert);
    }
}
