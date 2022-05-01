using MotoBest.Common.Extensions;
using MotoBest.Common.Units;

using MotoBest.Data.Models;
using MotoBest.Data.Seeding.Constants;

using MotoBest.Services.Common.Units;
using MotoBest.Services.Scraping.Models;

using static MotoBest.Common.GlobalConstants;
using static MotoBest.Services.Normalization.NormalizationConstants;

namespace MotoBest.Services.Normalization;

public class SiteDataNormalizer : ISiteDataNormalizer
{
    private static readonly Dictionary<string, string> engineVariations = new()
    {
        ["dizelov"] = EngineNames.Diesel,
        ["benzinov"] = EngineNames.Gasoline,
        ["elektricheski"] = EngineNames.Electric,
        ["hibriden"] = EngineNames.Hybrid,
    };

    private static readonly Dictionary<string, string> brandVariations = new()
    {
        ["Mercedes Benz"] = BrandNames.MercedesBenz
    };

    private readonly IUnitManager unitManager;

    public SiteDataNormalizer(IUnitManager unitManager)
    {
        this.unitManager = unitManager;
    }

    public NormalizedAdvert NormalizeAdvert(ScrapedAdvert scrapedAdvert)
        => new()
        {
            RemoteId = scrapedAdvert.RemoteId?.Trim(),
            RemoteSlug = scrapedAdvert.RemoteSlug,
            Title = NormalizeTitle(scrapedAdvert.Title),
            Description = scrapedAdvert.Description?.RemoveRepeatingWhiteSpaces(),
            Engine = NormalizeEngine(scrapedAdvert.Engine),
            BodyStyle = scrapedAdvert.BodyStyle?.Trim().ToLower(),
            Transmission = scrapedAdvert.Transmission?.Trim().ToLower(),
            Color = scrapedAdvert.Color?.Trim().ToLower(),
            Condition = scrapedAdvert.Condition?.Trim().ToLower(),
            PowerInHp = scrapedAdvert.Power,
            MileageInKm = scrapedAdvert.Mileage,
            ManufacturedOn = scrapedAdvert.ManufacturedOn,
            Region = NormalizeRegion(scrapedAdvert.Region),
            PopulatedPlace = NormalizePopulatedPlace(scrapedAdvert.PopulatedPlace),
            Brand = NormalizeBrand(scrapedAdvert.Brand),
            ModifiedOn = scrapedAdvert.ModifiedOn,
            PopulatedPlaceType = NormalizePopulatedPlaceType(scrapedAdvert.PopulatedPlace),
            PriceInBgn = NormalizePrice(scrapedAdvert.Price, scrapedAdvert.CurrencyUnit),
            ImageUrls = scrapedAdvert.ImageUrls.ToList(),
            EuroStandard = scrapedAdvert.EuroStandard?.Trim().ToLower(),
            Model = scrapedAdvert.Model?.Trim(),
            Site = scrapedAdvert.Site,
        };

    private decimal? NormalizePrice(decimal? price, CurrencyUnit? currencyUnit)
    {
        if (currencyUnit == null || price == null)
        {
            return price;
        }
        
        return price * unitManager.GetBgnCourse(currencyUnit.Value);
    }

    private static PopulatedPlaceType? NormalizePopulatedPlaceType(string? populatedPlace)
    {
        populatedPlace = populatedPlace?.Trim();

        if (populatedPlace == null)
        {
            return null;
        }

        if (populatedPlace.Contains(CityPrefix))
        {
            return PopulatedPlaceType.City;
        }

        if (populatedPlace.Contains(VillagePrefix))
        {
            return PopulatedPlaceType.Village;
        }
        
        return PopulatedPlaceType.Country;
    }

    private static string? NormalizeRegion(string? region)
        => region?.RemoveStrings(RegionPrefix).Trim();

    private static string? NormalizeBrand(string? brand)
    {
        brand = brand?.Trim();

        if (brand == null)
        {
            return null;
        }

        return brandVariations.ContainsKey(brand) ? brandVariations[brand] : brand;
    }

    private static string? NormalizePopulatedPlace(string? populatedPlace)
        => populatedPlace?.RemoveStrings(CityPrefix, VillagePrefix).Trim();

    private static string? NormalizeEngine(string? engine)
    {
        engine = engine?.Trim().ToLower();

        if (engine == null)
        {
            return null;
        }

        return engineVariations.ContainsKey(engine) ? engineVariations[engine] : engine;
    }

    private static string? NormalizeTitle(string? title)
    {
        title = title?.RemoveRepeatingWhiteSpaces();

        if (title == null)
        {
            return null;
        }

        string titleAndPriceInfoSeparator = $"{Whitespace}-{Whitespace}";
        string priceInfo = title.Split(titleAndPriceInfoSeparator).TakeLast(1).FirstOrDefault() ?? string.Empty;
        int startIndex = title.Length - priceInfo.Length - titleAndPriceInfoSeparator.Length;

        return title.Remove(startIndex);
    }
}
