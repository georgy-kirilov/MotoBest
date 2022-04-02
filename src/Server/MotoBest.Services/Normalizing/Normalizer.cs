using MotoBest.Common;

using MotoBest.Data.Seeding.Constants;

using MotoBest.Services.Scraping;

using System.Text;

namespace MotoBest.Services.Normalizing;

public class Normalizer : INormalizer
{
    private readonly ICurrencyCourseProvider currencyCourseProvider;

    private static readonly Dictionary<string, string> engineVariations = new()
    {
        ["dizelov"] = EngineNames.Diesel,
        ["benzinov"] = EngineNames.Gasoline,
        ["elektricheski"] = EngineNames.Electric,
        ["hibriden"] = EngineNames.Hybrid,
    };

    public Normalizer(ICurrencyCourseProvider currencyCourseProvider)
    {
        this.currencyCourseProvider = currencyCourseProvider;
    }

    public NormalizedAdvert Normalize(ScrapedAdvert scrapedAdvert)
        => new()
        {
            Title = scrapedAdvert.Title?.RemoveRepeatingWhiteSpaces(),
            Description = scrapedAdvert.Description?.RemoveRepeatingWhiteSpaces(),
            BodyStyle = scrapedAdvert.BodyStyle?.Trim().ToLower(),
            Color = scrapedAdvert.Color?.Trim().ToLower(),
            Condition = scrapedAdvert.Condition?.Trim().ToLower(),
            Engine = NormalizeEngine(scrapedAdvert.Engine),
            HorsePowers = scrapedAdvert.HorsePowers,
            Kilometrage = scrapedAdvert.Kilometrage,
            ManufacturedOn = scrapedAdvert.ManufacturedOn,
            Town = NormalizeTown(scrapedAdvert.Town),
            Region = NormalizeRegion(scrapedAdvert.Region),
            Transmission = scrapedAdvert.Transmission?.Trim().ToLower(),
            Brand = scrapedAdvert.Brand?.Trim(),
            ModifiedOn = scrapedAdvert.ModifiedOn,
            PriceBgn = NormalizePrice(scrapedAdvert.Price, scrapedAdvert.Currency),
            ImageUrls = scrapedAdvert.ImageUrls,
            EuroStandard = scrapedAdvert.EuroStandard?.Trim().ToLower(),
            RemoteId = scrapedAdvert.RemoteId?.Trim(),
            Model = scrapedAdvert.Model?.Trim(),
        };

    private decimal? NormalizePrice(decimal? price, Currency? currency)
    {
        if (currency == null)
        {
            return price;
        }

        if (price == null)
        {
            return null;
        }

        return price * currencyCourseProvider.GetCourseToBgn(currency.Value);
    }

    private static string? NormalizeRegion(string? region)
    {
        return region?.Sanitize("регион").Trim();
    }

    private static string? NormalizeTown(string? town)
    {
        return town?.Sanitize("с.", "гр.").Trim();
    }

    private static string? NormalizeEngine(string? engine)
    {
        if (engine == null)
        {
            return null;
        }

        engine = engine.Trim().ToLower()!;

        if (!engineVariations.ContainsKey(engine))
        {
            return null;
        }

        return engineVariations[engine];
    }
}
