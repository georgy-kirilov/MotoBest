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
            advert.Engine = dataCell.QuerySelector("a")?.GetAttribute("href")?.Split("/")[^1];
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
            string currencyAsText = dataCell.TextContent.Split(" ")[^1];

            if (currencyAsText == "лв.")
            {
                advert.Currency = Currency.Bgn;
            }

            string sanitizedValue = dataCell.TextContent
                .Replace(" ", string.Empty).Replace("лв.", string.Empty);

            if (decimal.TryParse(sanitizedValue, out decimal price))
            {
                advert.Price = price;
            }
        },

        ["модел"] = (dataCell, advert) =>
        {
            var modelArgs = dataCell.QuerySelector("a")?.GetAttribute("href")?.Split("/");

            if (modelArgs != null)
            {
                advert.Model = modelArgs[^1];
                advert.Brand = modelArgs[^2];
            }   
        }
    };

    public AdvertScrapeModel ScrapeAdvert(IDocument document)
    {
        var scrapeModel = new AdvertScrapeModel()
        {
            RemoteId = document.QuerySelector("title")?.TextContent.Split(" ")[^3],
            Title = document.QuerySelector("div.titleBig > h1")?.TextContent,
            Description = document.QuerySelector("div.moreInfo")?.TextContent,
            Town = document.QuerySelector("div.main > div.name")?.TextContent
        };

        var tableRows = document.QuerySelectorAll("div.carData > table.dowble > tbody > tr");

        var headers = tableRows
            .SelectMany(row => row.QuerySelectorAll("th"))
            .Select(header => header.TextContent.Trim().ToLower())
            .Where(header => header != string.Empty)
            .ToList();

        var tableDataCells = tableRows
            .SelectMany(row => row.QuerySelectorAll("td")).ToList();

        for (int i = 0; i < headers.Count; i++)
        {
            string header = headers[i];

            if (!mainDataParsingTable.ContainsKey(header))
            {
                continue;
            }

            mainDataParsingTable[header].Invoke(tableDataCells[i], scrapeModel);
        }

        return scrapeModel;
    }
}
