using MotoBest.Common;
using MotoBest.Common.Extensions;
using MotoBest.Common.Units;
using MotoBest.Data.Models;
using MotoBest.Data.Seeding.Constants;

using MotoBest.Services.Common;
using MotoBest.Services.Common.Units;
using MotoBest.Services.Scraping.Models;

using static MotoBest.Services.Normalization.NormalizationConstants;

namespace MotoBest.Services.Normalization;

public class SiteDataNormalizer : ISiteDataNormalizer
{
    private readonly ICurrencyCourseProvider currencyCourseProvider;

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

    public SiteDataNormalizer(ICurrencyCourseProvider currencyCourseProvider)
    {
        this.currencyCourseProvider = currencyCourseProvider;
    }

    public NormalizedAdvert NormalizeAdvert(ScrapedAdvert scrapedAdvert)
        => new()
        {
            Title = scrapedAdvert.Title?.RemoveRepeatingWhiteSpaces(),
            Description = scrapedAdvert.Description?.RemoveRepeatingWhiteSpaces(),
            BodyStyle = scrapedAdvert.BodyStyle?.Trim().ToLower(),
            Color = scrapedAdvert.Color?.Trim().ToLower(),
            Condition = scrapedAdvert.Condition?.Trim().ToLower(),
            Engine = NormalizeEngine(scrapedAdvert.Engine),
            PowerInHp = scrapedAdvert.Power,
            MileageInKm = scrapedAdvert.Mileage,
            ManufacturedOn = scrapedAdvert.ManufacturedOn,
            PopulatedPlace = NormalizePopulatedPlace(scrapedAdvert.PopulatedPlace),
            Region = NormalizeRegion(scrapedAdvert.Region),
            Transmission = scrapedAdvert.Transmission?.Trim().ToLower(),
            Brand = NormalizeBrand(scrapedAdvert.Brand),
            ModifiedOn = scrapedAdvert.ModifiedOn,
            PopulatedPlaceType = NormalizePopulatedPlaceType(scrapedAdvert.PopulatedPlace),
            PriceInBgn = NormalizePrice(scrapedAdvert.Price, scrapedAdvert.CurrencyUnit),
            ImageUrls = scrapedAdvert.ImageUrls,
            EuroStandard = scrapedAdvert.EuroStandard?.Trim().ToLower(),
            RemoteId = scrapedAdvert.RemoteId?.Trim(),
            Model = scrapedAdvert.Model?.Trim(),
            Site = scrapedAdvert.Site,
        };

    private decimal? NormalizePrice(decimal? price, CurrencyUnit? currencyUnit)
    {
        if (currencyUnit == null || price == null)
        {
            return price;
        }
        
        return price * currencyCourseProvider.GetBgnCourse(currencyUnit.Value);
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
}
