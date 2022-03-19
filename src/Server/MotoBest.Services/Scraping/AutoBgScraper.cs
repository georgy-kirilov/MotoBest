using AngleSharp.Dom;
using MotoBest.Common;
using System.Globalization;

namespace MotoBest.Services.Scraping;

public class AutoBgScraper : IScraper
{
    private static readonly Dictionary<string, Action<IElement, AdvertScrapeModel>> mainDataParsingTable = new()
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
            const string bgn = "лв.";
            string currencyAsText = dataCell.TextContent.Split(" ")[^1];

            if (currencyAsText == bgn)
            {
                advert.Currency = Currency.Bgn;
            }

            string sanitizedValue = dataCell.TextContent.Replace(" ", string.Empty).Replace(bgn, string.Empty);

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

    public AdvertScrapeModel ScrapeAdvert(IDocument document)
    {
        ScrapeMainData(document, out AdvertScrapeModel scrapeModel);
        ScrapeLocation(document, out string? region, out string? town);

        var urlArgs = document.QuerySelector("title")?.TextContent.Split(" ");

        if (urlArgs != null && urlArgs.Length >= 3)
        {
            scrapeModel.RemoteId = urlArgs[^3];
        }

        scrapeModel.Title = document.QuerySelector("div.titleBig > h1")?.TextContent;
        scrapeModel.Description = document.QuerySelector("div.moreInfo")?.TextContent;
        scrapeModel.Region = region;
        scrapeModel.Town = town;

        scrapeModel.ImageUrls = ScapeImageUrls(document);

        return scrapeModel;
    }

    private static void ScrapeMainData(IDocument document, out AdvertScrapeModel scrapeModel)
    {
        scrapeModel = new AdvertScrapeModel();

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
                mainDataParsingTable[header].Invoke(tableDataCells[i], scrapeModel);
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
}
