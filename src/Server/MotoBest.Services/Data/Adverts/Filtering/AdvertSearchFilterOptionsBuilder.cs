using MotoBest.Data.Models;
using MotoBest.Services.Data.AdvertFeatures;

namespace MotoBest.Services.Data.Adverts.Filtering;

public class AdvertSearchFilterOptionsBuilder : IAdvertSearchFilterBuilder, IAdvertSearchFilterOptionsBuilder
{
    private IQueryable<Advert> query;

    private readonly IAdvertFeatureService<Engine> engineService;
    private readonly IAdvertFeatureService<Transmission> transmissionService;
    private readonly IAdvertFeatureService<BodyStyle> bodyStyleService;
    private readonly IAdvertFeatureService<Color> colorService;
    private readonly IAdvertFeatureService<Condition> conditionService;
    private readonly IAdvertFeatureService<Brand> brandService;
    private readonly IAdvertFeatureService<Region> regionService;

    public AdvertSearchFilterOptionsBuilder(
        IAdvertFeatureService<Engine> engineService,
        IAdvertFeatureService<Transmission> transmissionService,
        IAdvertFeatureService<BodyStyle> bodyStyleService,
        IAdvertFeatureService<Color> colorService,
        IAdvertFeatureService<Condition> conditionService,
        IAdvertFeatureService<Brand> brandService,
        IAdvertFeatureService<Region> regionService)
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

    public IAdvertSearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts)
    {
        query = adverts;
        return this;
    }

    public IQueryable<Advert> ApplyFilter() => query;

    public IAdvertSearchFilterOptionsBuilder ByBodyStyle(string? bodyStyle)
    {
        var bodyStyleId = bodyStyleService.FindIdByName(bodyStyle);
        query = query.Where(a => bodyStyle == null || a.BodyStyleId == bodyStyleId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByBrand(string? brand)
    {
        var brandId = brandService.FindIdByName(brand);
        query = query.Where(a => brand == null || a.BrandId == brandId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByColor(string? color)
    {
        var colorId = colorService.FindIdByName(color);
        query = query.Where(a => color == null || a.ColorId == colorId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByCondition(string? condition)
    {
        var conditionId = conditionService.FindIdByName(condition);
        query = query.Where(a => condition == null || a.ConditionId == conditionId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByEngine(string? engine)
    {
        var engineId = engineService.FindIdByName(engine);
        query = query.Where(a => engine == null || a.EngineId == engineId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByRegion(string? region)
    {
        var regionId = regionService.FindIdByName(region);
        query = query.Where(a => region == null || a.RegionId == regionId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByTransmission(string? transmission)
    {
        var transmissionId = transmissionService.FindIdByName(transmission);
        query = query.Where(a => transmission == null || a.TransmissionId == transmissionId);
        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp)
    {
        query = query.Where(a => (minPowerInHp == null || a.PowerInHp >= minPowerInHp)
            && (maxPowerInHp == null || a.PowerInHp <= maxPowerInHp));

        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByKilometrage(int? minMileageInKm, int? maxMileageInKm)
    {
        query = query.Where(a => (minMileageInKm == null || a.MileageInKm >= minMileageInKm)
            && (maxMileageInKm == null || a.MileageInKm <= maxMileageInKm));

        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear)
    {
        query = query.Where(a => (minYear == null || (a.ManufacturedOn != null && a.ManufacturedOn.Value.Year >= minYear)
            && (maxYear == null || (a.ManufacturedOn != null && a.ManufacturedOn.Value.Year <= maxYear))));

        return this;
    }

    public IAdvertSearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn)
    {
        query = query.Where(a => (minPriceInBgn == null || a.PriceInBgn >= minPriceInBgn)
            && (maxPriceInBgn == null || a.PriceInBgn <= maxPriceInBgn));

        return this;
    }
}
