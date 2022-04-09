using System;

using MotoBest.Common;

using MotoBest.Services.Normalization;
using MotoBest.Services.Scraping.Models;

using MotoBest.Tests.Mocks;
using MotoBest.Tests.Scraping.DesktopAutoBg;

using Xunit;

namespace MotoBest.Tests.Normalizing;

public class SiteDataNormalizerTests
{
    [Theory]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_001))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_002))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_003))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_004))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_005))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_006))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_007))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_008))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_009))]
    [InlineData(typeof(DesktopAutoBgNormalizedAdverts), typeof(DesktopAutoBgScrapedAdverts), nameof(DesktopAutoBgNormalizedAdverts.Test_010))]
    public void NormalizeAdvert_ShouldReturn_CorrectResult(Type normalizedAdvertSource, Type scrapedAdvertSource, string normalizedAdvertTestName)
    {
        var expectedNormalizedAdvert = normalizedAdvertSource
            .GetField(normalizedAdvertTestName)?
            .GetValue(null) as NormalizedAdvert;

        var scrapedAdvert = scrapedAdvertSource
            .GetField(normalizedAdvertTestName)?
            .GetValue(null) as ScrapedAdvert;

        var actualNormalizedAdvert = new SiteDataNormalizer(
            new FakeCurrencyCourseProvider()).NormalizeAdvert(scrapedAdvert!);

        expectedNormalizedAdvert.AssertPropertyValues(actualNormalizedAdvert);
    }
}
