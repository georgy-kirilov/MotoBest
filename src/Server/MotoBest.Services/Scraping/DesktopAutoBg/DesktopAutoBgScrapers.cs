using AngleSharp.Dom;

using System.Text;
using System.Globalization;

using MotoBest.Services.Scraping.Models;

using MotoBest.Common.Extensions;
using MotoBest.Common.Units;

using static MotoBest.Common.GlobalConstants;
using static MotoBest.Services.Scraping.Common.ScrapingConstants.DesktopAutoBg;

namespace MotoBest.Services.Scraping.DesktopAutoBg;

internal static class DesktopAutoBgScrapers
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

        string brandWithSpaces = advert.Brand;
        string brandWithDashes = advert.Brand.Replace(Whitespace, "-");

        advert.Model = content
            .Replace(brandWithDashes, string.Empty, StringComparison.InvariantCultureIgnoreCase)
            .Replace(brandWithSpaces, string.Empty, StringComparison.InvariantCultureIgnoreCase)
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

    internal static IEnumerable<string> ScrapeImageUrls(IDocument document)
    {
        var imageUrls = document
            .QuerySelectorAll("#carGallery > .smallPhotos > ul > li")
            .Select(li => li.QuerySelector("img")?.GetAttribute("src"))
            .ToList();

        var bigImageUrl = document
            .QuerySelector("#carGallery > .bigPhoto > span > img")?
            .GetAttribute("src");

        imageUrls.Add(bigImageUrl);

        return imageUrls
            .Where(imageUrl => imageUrl != null && imageUrl != VipImageUrl)
            .Distinct()!;
    }

    internal static string? ScrapeTitle(IDocument document)
    {
        var title = document.QuerySelector("div.titleBig > h1")?.TextContent.Trim();
        return !string.IsNullOrEmpty(title) ? title : null;
    }

    internal static string? ScrapeDescription(IDocument document)
    {
        var description = document.QuerySelector("div.moreInfo")?.TextContent.Trim();
        return !string.IsNullOrEmpty(description) ? description : null;
    }

    internal static string? ScrapeRemoteId(IDocument document)
    {
        const int remoteIdIndex = 3;
        var urlArgs = ScrapeUrlArguments(document);
        return urlArgs.Length > remoteIdIndex ? urlArgs[remoteIdIndex] : null;
    }

    internal static string? ScrapeSlug(IDocument document)
    {
        const int slugIndex = 4;
        var urlArgs = ScrapeUrlArguments(document);
        return urlArgs.Length > slugIndex ? urlArgs[slugIndex] : null;
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

    internal static void ScrapeMileage(IElement mileageDomElement, ScrapedAdvert advert)
    {
        string mileageAsText = mileageDomElement
            .TextContent
            .ToLower()
            .RemoveStrings(KilometersSuffix)
            .Trim();

        advert.Mileage = ParseMileage(mileageAsText);
    }

    internal static void ScrapePower(IElement powerDomElement, ScrapedAdvert advert)
    {
        string powerAsText = powerDomElement
            .TextContent
            .ToLower()
            .RemoveStrings(HorsepowersSuffix)
            .Trim();

        advert.Power = ParsePower(powerAsText);
    }

    internal static void ScrapeManufacturedOnDate(IElement manufacturedOnDateDomElement, ScrapedAdvert advert)
    {
        string manufacturedOnDateAsText = manufacturedOnDateDomElement
            .TextContent
            .ToLower()
            .RemoveStrings(YearSuffix)
            .Trim();

        advert.ManufacturedOn = ParseManufacturedOnDate(manufacturedOnDateAsText);
    }

    internal static void ScrapePriceAndCurrency(IElement priceDomElement, ScrapedAdvert advert)
    {
        string priceAsText = priceDomElement.TextContent.ToLower();

        advert.Price = ParsePrice(priceAsText);
        advert.CurrencyUnit = ParseCurrency(priceAsText);
    }

    internal static ScrapedSearchAdvertResult? ScrapeAdvertResult(IElement resultItem, DateTime todayDate)
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

        if (modifiedOnDateAsText != TodayDateAsText)
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

        return new ScrapedSearchAdvertResult
        {
            Url = url,
            ModifiedOn = new DateTime(year, month, day, hour, minute, 0)
        };
    }

    private static int? ParseMileage(string mileageAsText)
    {
        bool isMileageValid = int.TryParse(mileageAsText, out int mileage);
        return isMileageValid ? mileage : null;
    }

    private static int? ParsePower(string powerAsText)
    {
        bool isPowerValid = int.TryParse(powerAsText, out int power);
        return isPowerValid ? power : null;
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
        if (priceAsText == NegotiablePrice)
        {
            return 0;
        }

        string sanitizedPriceAsText = priceAsText.RemoveStrings(Whitespace, Bgn, Eur);
        bool isPriceValid = decimal.TryParse(sanitizedPriceAsText, out decimal price);

        return isPriceValid ? price : null;
    }

    private static CurrencyUnit? ParseCurrency(string priceAsText)
    {
        var priceAsTextArgs = priceAsText.ToLower().Split(Whitespace);

        if (priceAsTextArgs.Length < 2)
        {
            return null;
        }

        string currencyUnitAsText = priceAsTextArgs[^1];

        return currencyUnitAsText switch
        {
            Bgn => CurrencyUnit.Bgn,
            Eur => CurrencyUnit.Eur,
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

    private static string[] ScrapeUrlArguments(IDocument document)
        => document.QuerySelector("meta[property='og:url']")?
            .GetAttribute("content")?
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select(arg => arg.Trim())
            .ToArray() ?? Array.Empty<string>();
}
