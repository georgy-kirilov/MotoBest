using AngleSharp.Dom;

using MotoBest.Data.Seeding.Constants;

using MotoBest.Services.Common;
using MotoBest.Services.Scraping.Common;
using MotoBest.Services.Scraping.Models;

using static MotoBest.Services.Scraping.DesktopAutoBg.DesktopAutoBgScrapers;
using static MotoBest.Services.Scraping.Common.ScrapingConstants.DesktopAutoBg;

namespace MotoBest.Services.Scraping.DesktopAutoBg;

public class DesktopAutoBgSiteScraper : ISiteScraper
{
    private static readonly Dictionary<string, Action<IElement, ScrapedAdvert>> mainAdvertDataScrapingTable = new()
    {
        [ColorLabel] = ScrapeColor,
        [BodyStyleLabel] = ScrapeBodyStyle,
        [TransmissionLabel] = ScrapeTransmission,
        [EngineLabel] = ScrapeEngine,
        [ConditionLabel] = ScrapeCondition,
        [MileageLabel] = ScrapeMileage,
        [PowerLabel] = ScrapePower,
        [ManufacturedOnDateLabel] = ScrapeManufacturedOnDate,
        [PriceLabel] = ScrapePriceAndCurrency,
        [ModelLabel] = ScrapeBrandAndModel,
    };

    private readonly IDateTimeManager dateTimeManager;

    public DesktopAutoBgSiteScraper(IDateTimeManager dateTimeManager)
    {
        this.dateTimeManager = dateTimeManager;
    }

    public ScrapedAdvert ScrapeAdvert(IDocument document)
    {
        var (region, populatedPlace) = ScrapeLocation(document);

        var scrapedAdvert = new ScrapedAdvert
        {
            Region = region,
            PopulatedPlace = populatedPlace,
            RemoteId = ScrapeRemoteId(document),
            Slug = ScrapeSlug(document),
            Title = ScrapeTitle(document),
            Description = ScrapeDescription(document),
            ImageUrls = ScrapeImageUrls(document),
            Site = SiteNames.AutoBg,
        };

        ScrapeMainAdvertData(document, scrapedAdvert);
        return scrapedAdvert;
    }

    public IEnumerable<ScrapedSearchAdvertResult?> ScrapeAdvertSearchResults(IDocument document)
        => document
            .QuerySelectorAll("#resultsPage > ul > #rightColumn > .results > .resultItem")
            .Select(item => ScrapeAdvertResult(item, dateTimeManager.Today()));

    public IEnumerable<string> ScrapeAllModels(IDocument document)
    {
        var divs = document.QuerySelectorAll("#resultsPage > .scheme13 > #rightColumn > .results > .similar > div");

        if (divs?.Length < 2)
        {
            return Array.Empty<string>();
        }

        return divs![1]
            .QuerySelectorAll("a")?
            .Select(a => a.TextContent.Trim())
            .Distinct() ?? Array.Empty<string>();
    }

    private static void ScrapeMainAdvertData(IDocument document, ScrapedAdvert scrapedAdvert)
    {
        var tableRows = document.QuerySelectorAll("div.carData > table.dowble > tbody > tr");

        var headers = tableRows
            .SelectMany(row => row.QuerySelectorAll("th"))
            .Select(header => header.TextContent.Trim().ToLower())
            .Where(header => header != string.Empty)
            .ToList();

        var tableDataCells = tableRows
            .SelectMany(row => row.QuerySelectorAll("td"))
            .ToList();

        for (int i = 0; i < headers.Count; i++)
        {
            string header = headers[i];

            if (!mainAdvertDataScrapingTable.ContainsKey(header))
            {
                continue;
            }

            mainAdvertDataScrapingTable[header].Invoke(tableDataCells[i], scrapedAdvert);
        }
    }
}
