using MotoBest.Data.Models;
using MotoBest.Services.Data.Features;

namespace MotoBest.Services.Data.Adverts.Filtering;

public class SearchFilterOptionsBuilder : ISearchFilterBuilder, ISearchFilterOptionsBuilder
{
    private IQueryable<Advert> query;

    private readonly IFeatureService<Engine> engineService;
    private readonly IFeatureService<Transmission> transmissionService;
    private readonly IFeatureService<BodyStyle> bodyStyleService;
    private readonly IFeatureService<Color> colorService;
    private readonly IFeatureService<Condition> conditionService;
    private readonly IFeatureService<Brand> brandService;
    private readonly IFeatureService<Region> regionService;

    public SearchFilterOptionsBuilder(
        IFeatureService<Engine> engineService,
        IFeatureService<Transmission> transmissionService,
        IFeatureService<BodyStyle> bodyStyleService,
        IFeatureService<Color> colorService,
        IFeatureService<Condition> conditionService,
        IFeatureService<Brand> brandService,
        IFeatureService<Region> regionService)
    {
        query = new List<Advert>().AsQueryable();

        this.engineService = engineService;
        this.transmissionService = transmissionService;
        this.bodyStyleService = bodyStyleService;
        this.colorService = colorService;
        this.conditionService = conditionService;
        this.brandService = brandService;
        this.regionService = regionService;
    }

    public ISearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts)
    {
        query = adverts;
        return this;
    }

    public IQueryable<Advert> ApplyFilter() => query;

    public ISearchFilterOptionsBuilder ByBodyStyle(string? bodyStyle)
    {
        var bodyStyleId = bodyStyleService.FindIdByName(bodyStyle);
        query = query.Where(a => bodyStyle == null || a.BodyStyleId == bodyStyleId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByColor(string? color)
    {
        var colorId = colorService.FindIdByName(color);
        query = query.Where(a => color == null || a.ColorId == colorId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByCondition(string? condition)
    {
        var conditionId = conditionService.FindIdByName(condition);
        query = query.Where(a => condition == null || a.ConditionId == conditionId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByEngine(string? engine)
    {
        var engineId = engineService.FindIdByName(engine);
        query = query.Where(a => engine == null || a.EngineId == engineId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByTransmission(string? transmission)
    {
        var transmissionId = transmissionService.FindIdByName(transmission);
        query = query.Where(a => transmission == null || a.TransmissionId == transmissionId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp)
    {
        query = query.Where(a =>
            (minPowerInHp == null || a.PowerInHp >= minPowerInHp)
            && (maxPowerInHp == null || a.PowerInHp <= maxPowerInHp));

        return this;
    }

    public ISearchFilterOptionsBuilder ByKilometrage(int? minMileageInKm, int? maxMileageInKm)
    {
        query = query.Where(a =>
            (minMileageInKm == null || a.MileageInKm >= minMileageInKm)
            && (maxMileageInKm == null || a.MileageInKm <= maxMileageInKm));

        return this;
    }

    public ISearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear)
    {
        query = query.Where(a =>
            (minYear == null || (a.ManufacturedOn != null && a.ManufacturedOn.Value.Year >= minYear))
            && (maxYear == null || (a.ManufacturedOn != null && a.ManufacturedOn!.Value.Year <= maxYear)));

        return this;
    }

    public ISearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn)
    {
        query = query.Where(a =>
            (minPriceInBgn == null || a.PriceInBgn >= minPriceInBgn)
            && (maxPriceInBgn == null || a.PriceInBgn <= maxPriceInBgn));

        return this;
    }

    public ISearchFilterOptionsBuilder ByMaking(string? brand, int? modelId)
    {
        if (modelId != null)
        {
            query = query.Where(a => a.ModelId == modelId);
        }
        else
        {
            var brandId = brandService.FindByName(brand)?.Id;
            query = query.Where(a => brand == null || a.BrandId == brandId);
        }
        
        return this;
    }

    public ISearchFilterOptionsBuilder ByLocation(string? region, int? populatedPlaceId)
    {
        if (populatedPlaceId != null)
        {
            query = query.Where(a => a.PopulatedPlaceId == populatedPlaceId);
        }
        else
        {
            var regionId = regionService.FindByName(region)?.Id;
            query = query.Where(a => region == null || a.RegionId == regionId);
        }

        return this;
    }
}
