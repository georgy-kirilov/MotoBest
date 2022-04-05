using AngleSharp.Dom;

using MotoBest.Common;
using MotoBest.Services.Scraping.Models;
using System.Globalization;
using System.Text;

using static MotoBest.Common.GlobalConstants;
using static MotoBest.Services.Scraping.ScrapingConstants.AutoBg;

namespace MotoBest.Services.Scraping;

internal static class AutoBgPropertyScrapers
{
    internal static void ScrapeBodyStyle(IElement bodyStyleDomElement, ScrapedAdvert advert)
        => advert.BodyStyle = bodyStyleDomElement.TextContent.Trim();

    internal static void ScrapeTransmission(IElement transmissionDomElement, ScrapedAdvert advert)
        => advert.Transmission = transmissionDomElement.TextContent.Trim();
    
    internal static void ScrapeColor(IElement colorDomElement, ScrapedAdvert advert)
        => advert.Color = colorDomElement.TextContent.Trim();

    internal static void ScrapeCondition(IElement conditionDomElement, ScrapedAdvert advert)
        => advert.Condition = conditionDomElement.TextContent.Trim();

    internal static void ScrapeBrandAndModel(IElement domElement, ScrapedAdvert advert)
    {
        var anchor = domElement.QuerySelector("a");
        var arguments = anchor?.GetAttribute("href")?.Split("/");
        string? content = anchor?.TextContent;

        if (arguments == null || arguments.Length < 2 || content == null)
        {
            return;
        }

        advert.Brand = ParseBrand(arguments[^2]);

        string originalBrand = advert.Brand.Replace(Whitespace, "-");

        advert.Model = content
            .RemoveMany(
                advert.Brand,
                originalBrand,
                originalBrand.ToUpper())
            .Trim();
    }

    internal static (string? region, string? populatedPlace) ScrapeLocation(IDocument document)
    {
        string? region = null, populatedPlace = null;

        var locationArgs = document
            .QuerySelectorAll("#leftColumn > #callDealerInside > div.main > div.name")[1]?
            .TextContent
            .Split(",")
            .Select(arg => arg.Trim())
            .ToList();

        if (locationArgs == null)
        {
            return (region, populatedPlace);
        }

        if (locationArgs.Count >= 1)
        {
            populatedPlace = locationArgs[0];
        }

        if (locationArgs.Count >= 2)
        {
            region = locationArgs[1];
        }

        return (region, populatedPlace);
    }

    internal static IEnumerable<string> ScapeImageUrls(IDocument document)
    {
        var imageUrls = document
            .QuerySelectorAll("#carGallery > .smallPhotos > ul > li")
            .Select(li => li.QuerySelector("img")?.GetAttribute("src"))
            .ToList();

        var bigImageUrl = document
            .QuerySelector("#carGallery > .bigPhoto > span > img")?
            .GetAttribute("src");

        imageUrls.Add(bigImageUrl);

        return imageUrls.Where(imageUrl => imageUrl != null).Distinct()!;
    }

    internal static string? ScrapeTitle(IDocument document)
    {
        var title = document.QuerySelector("div.titleBig > h1")?.TextContent.Trim();

        if (string.IsNullOrEmpty(title))
        {
            return null;
        }

        return title.Trim();
    }

    internal static string? ScrapeDescription(IDocument document)
    {
        var description = document.QuerySelector("div.moreInfo")?.TextContent.Trim();

        if (string.IsNullOrEmpty(description))
        {
            return null;
        }

        return description.Trim();
    }

    internal static string? ScrapeRemoteId(IDocument document)
    {
        var urlArgs = document.QuerySelector("title")?.TextContent.Split(Whitespace);

        if (urlArgs == null || urlArgs.Length < 3)
        {
            return null;
        }

        return urlArgs[^3].Trim();
    }

    internal static void ScrapeEngine(IElement engineDomElement, ScrapedAdvert advert)
    {
        var engineInfoArgs = engineDomElement
            .QuerySelector("a")?
            .GetAttribute("href")?
            .Split("/");

        if (engineInfoArgs?.Length > 0)
        {
            advert.Engine = engineInfoArgs[^1].Trim();
        }
    }

    internal static void ScrapeKilometrage(IElement kilometrageDomElement, ScrapedAdvert advert)
    {
        string kilometrageAsText = kilometrageDomElement
            .TextContent
            .ToLower()
            .RemoveMany(KilometersLabel)
            .Trim();

        advert.Kilometrage = ParseKilometrage(kilometrageAsText);
    }

    internal static void ScrapeHorsePowers(IElement horsePowersDomElement, ScrapedAdvert advert)
    {
        string horsePowersAsText = horsePowersDomElement
            .TextContent
            .ToLower()
            .RemoveMany(HorsePowersLabel)
            .Trim();

        advert.HorsePowers = ParseHorsePowers(horsePowersAsText);
    }

    internal static void ScrapeManufacturedOnDate(IElement manufacturedOnDateDomElement, ScrapedAdvert advert)
    {
        string manufacturedOnDateAsText = manufacturedOnDateDomElement
            .TextContent
            .ToLower()
            .RemoveMany(YearLabel)
            .Trim();

        advert.ManufacturedOn = ParseManufacturedOnDate(manufacturedOnDateAsText);
    }

    internal static void ScrapePriceAndCurrency(IElement priceDomElement, ScrapedAdvert advert)
    {
        string priceAsText = priceDomElement.TextContent.ToLower();

        advert.Price = ParsePrice(priceAsText);
        advert.Currency = ParseCurrency(priceAsText);
    }

    internal static SearchAdvertResult? ScrapeAdvertResult(IElement resultItem, DateTime todayDate)
    {
        string urlQuery = ".text > .head > .link > a";
        string modifiedOnQuery = ".text > .info > .date";

        string? url = resultItem.QuerySelector(urlQuery)?.GetAttribute("href");
        string? modifiedOnAsText = resultItem.QuerySelector(modifiedOnQuery)?.TextContent;

        if (modifiedOnAsText == null || url == null)
        {
            return null;
        }

        var modifiedOnArgs = modifiedOnAsText.Split(Whitespace);
        var modifiedOnTimeArgs = modifiedOnArgs[0].Split(":");

        bool hourValid = int.TryParse(modifiedOnTimeArgs[0], out int hour);
        bool minuteValid = int.TryParse(modifiedOnTimeArgs[1], out int minute);

        string modifiedOnDateAsText = modifiedOnArgs[3].ToLower().Trim();

        int day = todayDate.Day;
        int month = todayDate.Month;
        int year = todayDate.Year;

        if (modifiedOnDateAsText != TodayTextLabel)
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

        return new SearchAdvertResult
        {
            Url = url,
            ModifiedOn = new DateTime(year, month, day, hour, minute, 0)
        };
    }

    private static int? ParseKilometrage(string kilometrageAsText)
    {
        bool isKilometrageValid = int.TryParse(kilometrageAsText, out int kilometrage);
        return isKilometrageValid ? kilometrage : null;
    }

    private static int? ParseHorsePowers(string horsePowersAsText)
    {
        bool areHorsePowersValid = int.TryParse(horsePowersAsText, out int horsePowers);
        return areHorsePowersValid ? horsePowers : null;
    }

    private static DateTime? ParseManufacturedOnDate(string manufacturedOnDateAsText)
    {
        bool isManufacturedOnDateValid = DateTime.TryParse(
            manufacturedOnDateAsText,
            new CultureInfo(BulgarianCultureInfo),
            DateTimeStyles.None,
            out DateTime manufacturedOnDate);

        return isManufacturedOnDateValid ? manufacturedOnDate : null;
    }

    private static decimal? ParsePrice(string priceAsText)
    {
        if (priceAsText == "цена по договаряне")
        {
            return 0;
        }

        string sanitizedPriceAsText = priceAsText.RemoveMany(Whitespace, Bgn, Eur);
        bool isPriceValid = decimal.TryParse(sanitizedPriceAsText, out decimal price);

        return isPriceValid ? price : null;
    }

    private static Currency? ParseCurrency(string priceAsText)
    {
        var priceAsTextArgs = priceAsText.ToLower().Split(Whitespace);

        if (priceAsTextArgs.Length < 2)
        {
            return null;
        }

        string currencyAsText = priceAsTextArgs[^1];

        return currencyAsText switch
        {
            Bgn => Currency.Bgn,
            Eur => Currency.Eur,
            _ => null
        };
    }

    private static string ParseBrand(string rawBrand)
    {
        var brandWords = rawBrand.Split("-");
        var brandBuilder = new StringBuilder();

        foreach (string word in brandWords)
        {
            string firstLetter = word.First().ToString().ToUpper();

            brandBuilder.Append(firstLetter);

            for (int i = 1; i < word.Length; i++)
            {
                brandBuilder.Append(word[i]);
            }

            brandBuilder.Append(Whitespace);
        }

        return brandBuilder.ToString().Trim();
    }
}
