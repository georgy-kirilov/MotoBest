using AngleSharp.Dom;

using MotoBest.Common;

using System.Globalization;

namespace MotoBest.Services.Scraping;

public class AutoBgScraper : IScraper
{
    private static readonly Dictionary<string, Action<IElement, ScrapedAdvert>> mainDataParsingTable = new()
    {
        ["тип"] = (dataCell, advert) => advert.BodyStyle = dataCell.TextContent,

        ["скоростна кутия"] = (dataCell, advert) => advert.Transmission = dataCell.TextContent,

        ["тип двигател"] = (dataCell, advert) =>
        {
            var arguments = dataCell.QuerySelector("a")?.GetAttribute("href")?.Split("/");

            if (arguments?.Length > 0)
            {
                advert.Engine = arguments[^1];
            }
        },

        ["състояние"] = (dataCell, advert) => advert.Condition = dataCell.TextContent,

        ["пробег"] = (dataCell, advert) =>
        {
            string sanitizedValue = dataCell.TextContent.ToLower().Replace("км", string.Empty).Trim();

            if (int.TryParse(sanitizedValue, out int kilometrage))
            {
                advert.Kilometrage = kilometrage;
            }
        },

        ["мощност[к.с.]"] = (dataCell, advert) =>
        {
            string sanitizedValue = dataCell.TextContent.ToLower().Replace("к.с", string.Empty).Trim();

            if (int.TryParse(sanitizedValue, out int horsePowers))
            {
                advert.HorsePowers = horsePowers;
            }
        },

        ["цвят"] = (dataCell, advert) => advert.Color = dataCell.TextContent,

        ["произведено"] = (dataCell, advert) =>
        {
            string sanitizedValue = dataCell.TextContent.ToLower().Replace("г.", string.Empty).Trim();
            var cultureInfo = new CultureInfo("bg-BG");

            bool isDateValid = DateTime.TryParse(
                sanitizedValue, cultureInfo, DateTimeStyles.None, out DateTime manufacturedOn);

            if (isDateValid)
            {
                advert.ManufacturedOn = manufacturedOn;
            }
        },

        ["цена"] = (dataCell, advert) =>
        {
            string lowercaseText = dataCell.TextContent.ToLower();

            if (lowercaseText == "цена по договаряне")
            {
                advert.Price = 0;
                return;
            }

            const string bgn = "лв.";
            const string eur = "eur";

            string currencyAsText = lowercaseText.Split(" ")[^1];

            if (currencyAsText == bgn)
            {
                advert.Currency = Currency.Bgn;
            }

            if (currencyAsText == eur)
            {
                advert.Currency = Currency.Eur;
            }

            string sanitizedValue = lowercaseText
                .Replace(" ", string.Empty)
                .Replace(bgn, string.Empty)
                .Replace(eur, string.Empty);

            if (decimal.TryParse(sanitizedValue, out decimal price))
            {
                advert.Price = price;
            }
        },

        ["модел"] = (dataCell, advert) =>
        {
            var modelArgs = dataCell.QuerySelector("a")?.GetAttribute("href")?.Split("/");

            if (modelArgs != null && modelArgs.Length >= 2)
            {
                advert.Model = modelArgs[^1];
                advert.Brand = modelArgs[^2];
            }
        }
    };

    private readonly IDateTimeManager dateTimeManager;

    public AutoBgScraper(IDateTimeManager dateTimeManager)
    {
        this.dateTimeManager = dateTimeManager;
    }

    public ScrapedAdvert ScrapeAdvert(IDocument document)
    {
        ScrapeMainData(document, out ScrapedAdvert scrapedAdvert);
        ScrapeLocation(document, out string? region, out string? town);

        var urlArgs = document.QuerySelector("title")?.TextContent.Split(" ");

        if (urlArgs != null && urlArgs.Length >= 3)
        {
            scrapedAdvert.RemoteId = urlArgs[^3];
        }

        var description = document.QuerySelector("div.moreInfo")?.TextContent;

        if (description != null && description.Trim() != string.Empty)
        {
            scrapedAdvert.Description = description;
        }

        var title = document.QuerySelector("div.titleBig > h1")?.TextContent;

        if (title != null && title.Trim() != string.Empty)
        {
            scrapedAdvert.Title = title;
        }

        scrapedAdvert.Region = region;
        scrapedAdvert.Town = town;

        scrapedAdvert.ImageUrls = ScapeImageUrls(document);
        return scrapedAdvert;
    }

    public IEnumerable<AdvertResult?> ScrapeAdvertResultsFromPage(IDocument document)
        => document
            .QuerySelectorAll("#resultsPage > ul > #rightColumn > .results > .resultItem")
            .Select(ScrapeAdvertResult);

    private static void ScrapeMainData(IDocument document, out ScrapedAdvert scrapedAdvert)
    {
        scrapedAdvert = new ScrapedAdvert();

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

            if (mainDataParsingTable.ContainsKey(header))
            {
                mainDataParsingTable[header].Invoke(tableDataCells[i], scrapedAdvert);
            }
        }
    }

    private static void ScrapeLocation(IDocument document, out string? region, out string? town)
    {
        region = null;
        town = null;

        var locationArgs = document
            .QuerySelectorAll("#leftColumn > #callDealerInside > div.main > div.name")[1]?
            .TextContent
            .Split(",")
            .Select(arg => arg.Trim())
            .ToList();

        if (locationArgs == null)
        {
            return;
        }

        if (locationArgs.Count >= 1)
        {
            town = locationArgs[0];
        }

        if (locationArgs.Count >= 2)
        {
            region = locationArgs[1];
        }
    }

    private static IEnumerable<string> ScapeImageUrls(IDocument document)
    {
        var imageUrls = document
            .QuerySelectorAll("#carGallery > .smallPhotos > ul > li")
            .Select(li => li.QuerySelector("img")?.GetAttribute("src"))
            .ToList();

        var bigImageUrl = document.QuerySelector("#carGallery > .bigPhoto > span > img")?.GetAttribute("src");
        imageUrls.Add(bigImageUrl);

        return imageUrls.Where(imageUrl => imageUrl != null).Distinct().ToList()!;
    }

    private AdvertResult? ScrapeAdvertResult(IElement resultItem)
    {
        string urlQuery = ".text > .head > .link > a";
        string modifiedOnQuery = ".text > .info > .date";

        string? url = resultItem.QuerySelector(urlQuery)?.GetAttribute("href");
        string? modifiedOnAsText = resultItem.QuerySelector(modifiedOnQuery)?.TextContent;

        if (modifiedOnAsText == null || url == null)
        {
            return null;
        }

        var modifiedOnArgs = modifiedOnAsText.Split(" ");
        var modifiedOnTimeArgs = modifiedOnArgs[0].Split(":");

        bool hourValid = int.TryParse(modifiedOnTimeArgs[0], out int hour);
        bool minuteValid = int.TryParse(modifiedOnTimeArgs[1], out int minute);

        string modifiedOnDateAsText = modifiedOnArgs[3].ToLower().Trim();

        var today = dateTimeManager.Today();
        int day = today.Day;
        int month = today.Month;
        int year = today.Year;

        if (modifiedOnDateAsText != "днес")
        {
            var modifiedOnDateArgs = modifiedOnDateAsText.Split(".");

            bool dayValid = int.TryParse(modifiedOnDateArgs[0], out day);
            bool monthValid = int.TryParse(modifiedOnDateArgs[1], out month);
            bool yearValid = int.TryParse(modifiedOnDateArgs[2], out year);

            if (!hourValid || !minuteValid || !dayValid || !monthValid || !yearValid)
            {
                return null;
            }
        }

        return new AdvertResult
        {
            Url = url,
            ModifiedOn = new DateTime(year, month, day, hour, minute, 0)
        };
    }
}
