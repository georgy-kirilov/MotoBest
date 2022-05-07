using AutoMapper;

using Microsoft.EntityFrameworkCore;
using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.Services.Data.Features;
using MotoBest.Services.Data.Adverts.Filtering;
using MotoBest.Services.Data.Adverts.Models;

using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data.Adverts;

public class AdvertService : IAdvertService
{
    private readonly IMapper mapper;

    private readonly IEuroStandardService euroStandards;
    private readonly IPopulatedPlaceService populatedPlaces;

    private readonly IRepository<Advert> adverts;
    private readonly IRepository<Image> images;

    private readonly IFeatureService<Transmission> transmissions;
    private readonly IFeatureService<BodyStyle> bodyStyles;

    private readonly IFeatureService<Engine> engines;
    private readonly IFeatureService<Condition> conditions;

    private readonly IFeatureService<Color> colors;
    private readonly IFeatureService<Brand> brands;

    private readonly IFeatureService<Site> sites;
    private readonly ISearchFilterFactory searchFilter;

    public AdvertService(
        IMapper mapper,
        IEuroStandardService euroStandards,
        IPopulatedPlaceService populatedPlaces,
        IRepository<Advert> adverts,
        IRepository<Image> images,
        IFeatureService<Transmission> transmissions,
        IFeatureService<BodyStyle> bodyStyles,
        IFeatureService<Engine> engines,
        IFeatureService<Condition> conditions,
        IFeatureService<Color> colors,
        IFeatureService<Brand> brands,
        IFeatureService<Site> sites,
        ISearchFilterFactory searchFilter)
    {
        this.mapper = mapper;
        this.adverts = adverts;
        this.images = images;
        this.transmissions = transmissions;
        this.bodyStyles = bodyStyles;
        this.engines = engines;
        this.conditions = conditions;
        this.colors = colors;
        this.euroStandards = euroStandards;
        this.populatedPlaces = populatedPlaces;
        this.brands = brands;
        this.sites = sites;
        this.searchFilter = searchFilter;
    }

    public async Task AddOrUpdate(NormalizedAdvert normalizedAdvert)
    {
        var siteId = sites.FindIdByName(normalizedAdvert.Site);
        var transmissionId = transmissions.FindIdByName(normalizedAdvert.Transmission);

        var engineId = engines.FindIdByName(normalizedAdvert.Engine);
        var bodyStyleId = bodyStyles.FindIdByName(normalizedAdvert.BodyStyle);

        var colorId = colors.FindIdByName(normalizedAdvert.Color);
        var conditionId = conditions.FindIdByName(normalizedAdvert.Condition);

        var brand = brands.FindByName(normalizedAdvert.Brand);
        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        var (euroStandard, isEuroStandardApproximate) = await FindOrApproximateEuroStandard(
            normalizedAdvert.EuroStandard,
            normalizedAdvert.ManufacturedOn);

        var populatedPlace = populatedPlaces.FindByRegion(
            normalizedAdvert.Region,
            normalizedAdvert.PopulatedPlace,
            normalizedAdvert.PopulatedPlaceType);

        var images = normalizedAdvert.ImageUrls
            .Select(url => new Image { Url = url })
            .ToList();

        var (advert, doesAdvertExist) = await FindAdvertBySiteInfo(normalizedAdvert.RemoteId, siteId);

        DeleteAdvertImages(advert.Images.ToList());

        advert.Images = images;

        advert.SiteId = siteId;
        advert.RemoteId = normalizedAdvert.RemoteId;
        advert.RemoteSlug = normalizedAdvert.RemoteSlug;

        advert.Title = normalizedAdvert.Title;
        advert.Description = normalizedAdvert.Description;

        advert.ColorId = colorId;
        advert.ConditionId = conditionId;

        advert.EngineId = engineId;
        advert.PriceInBgn = normalizedAdvert.PriceInBgn;

        advert.BodyStyleId = bodyStyleId;
        advert.TransmissionId = transmissionId;

        advert.ModifiedOn = normalizedAdvert.ModifiedOn;
        advert.ManufacturedOn = normalizedAdvert.ManufacturedOn;

        advert.PowerInHp = normalizedAdvert.PowerInHp;
        advert.MileageInKm = normalizedAdvert.MileageInKm;

        advert.RegionId = populatedPlace?.RegionId;
        advert.PopulatedPlaceId = populatedPlace?.Id;

        advert.BrandId = brand?.Id;
        advert.ModelId = model?.Id;

        advert.EuroStandardId = euroStandard?.Id;
        advert.IsEuroStandardApproximate = isEuroStandardApproximate;

        if (!doesAdvertExist)
        {
            await adverts.AddAsync(advert);
        }

        await adverts.SaveChangesAsync();
    }

    public async Task<GetFullAdvertResultModel?> GetFullAdvert(string id)
    {
        var advert = await adverts.All().FirstOrDefaultAsync(a => a.Id == id);
        return mapper.Map<GetFullAdvertResultModel>(advert);
    }

    public DateTime? FindLatestAdvertModifiedOnDate(string site)
        => FindAdvertsBySiteId(sites.FindIdByName(site))
            .FirstOrDefault()?
            .ModifiedOn;

    public IEnumerable<SearchAdvertsResultModel> SearchAdverts(
        SearchAdvertsInputModel input,
        int pageIndex,
        int resultsPerPageCount)
        => FilterAdvertsBy(input)
            .Skip(count: pageIndex * resultsPerPageCount)
            .Take(resultsPerPageCount)
            .ToList()
            .Select(mapper.Map<Advert, SearchAdvertsResultModel>);

    private IQueryable<Advert> FilterAdvertsBy(SearchAdvertsInputModel input)
        => searchFilter
            .CreateFilterFor(adverts.All())
            .ByBodyStyle(input.BodyStyleId)
            .ByMaking(input.BrandId, input.ModelId)
            .ByColor(input.ColorId)
            .ByCondition(input.ConditionId)
            .ByEuroStandard(input.EuroStandardId)
            .ByEngine(input.EngineId)
            .ByLocation(input.RegionId, input.PopulatedPlaceId)
            .ByTransmission(input.TransmissionId)
            .ByPower(input.MinPowerInHp, input.MaxPowerInHp)
            .ByMileage(input.MinMileageInKm, input.MaxMileageInKm)
            .ByYear(input.MinYear, input.MaxYear)
            .ByPrice(input.MinPriceInBgn, input.MaxPriceInBgn)
            .ApplyFilter();

    private IQueryable<Advert> FindAdvertsBySiteId(int? siteId)
        => adverts.All().Where(a => a.SiteId == siteId);

    private async Task<(Advert advert, bool doesAdvertExist)> FindAdvertBySiteInfo(string? remoteId, int? siteId)
    {
        var advert = await adverts.All()
            .FirstOrDefaultAsync(a => a.RemoteId == remoteId && a.SiteId == siteId) ?? new Advert();

        bool doesAdvertExist = adverts.All()
            .Any(a => a.RemoteId == remoteId && a.SiteId == siteId);

        return (advert, doesAdvertExist);
    }

    private async Task<(EuroStandard? euroStandard, bool isApproximate)> FindOrApproximateEuroStandard(
        string? euroStandardName,
        DateTime? manufacturedOn)
    {
        bool isApproximate = false;
        var euroStandard = euroStandards.FindByName(euroStandardName);

        if (euroStandard == null)
        {
            isApproximate = true;
            euroStandard = await euroStandards.Approximate(manufacturedOn);
        }

        return (euroStandard, isApproximate);
    }

    private void DeleteAdvertImages(IList<Image> imagesToDelete)
    {
        foreach (var imageToDelete in imagesToDelete)
        {
            images.Delete(imageToDelete);
        }
    }
}
