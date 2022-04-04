using AngleSharp.Dom;

using static MotoBest.Services.Scraping.AutoBgPropertyScrapers;

namespace MotoBest.Services.Scraping;

public class AutoBgScraper : IScraper
{
    private static readonly Dictionary<string, Action<IElement, ScrapedAdvert>> mainAdvertDataScrapingTable = new()
    {
        ["тип"] = (domElement, advert) => advert.BodyStyle = domElement.TextContent,
        ["скоростна кутия"] = (domElement, advert) => advert.Transmission = domElement.TextContent,
        ["тип двигател"] = ScrapeEngine,
        ["състояние"] = (domElement, advert) => advert.Condition = domElement.TextContent,
        ["пробег"] = ScrapeKilometrage,
        ["мощност[к.с.]"] = ScrapeHorsePowers,
        ["цвят"] = (domElement, advert) => advert.Color = domElement.TextContent,
        ["произведено"] = ScrapeManufacturedOnDate,
        ["цена"] = ScrapePriceAndCurrency,
        ["модел"] = ScrapeBrandAndModel,
    };

    private readonly IDateTimeManager dateTimeManager;

    public AutoBgScraper(IDateTimeManager dateTimeManager)
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
            Title = ScrapeTitle(document),
            Description = ScrapeDescription(document),
            ImageUrls = ScapeImageUrls(document),
        };

        ScrapeMainAdvertData(document, scrapedAdvert);
        return scrapedAdvert;
    }

    public IEnumerable<AdvertResult?> ScrapeAdvertResultsFromPage(IDocument document)
        => document
            .QuerySelectorAll("#resultsPage > ul > #rightColumn > .results > .resultItem")
            .Select(item => ScrapeAdvertResult(item, dateTimeManager.Today()));

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
