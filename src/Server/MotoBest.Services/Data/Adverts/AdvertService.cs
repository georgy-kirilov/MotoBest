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

    private readonly IEuroStandardService euroStandardService;
    private readonly IPopulatedPlaceService populatedPlaceService;

    private readonly IRepository<Advert> advertRepository;
    private readonly IRepository<Image> imageRepository;

    private readonly IFeatureService<Transmission> transmissionService;
    private readonly IFeatureService<BodyStyle> bodyStyleService;

    private readonly IFeatureService<Engine> engineService;
    private readonly IFeatureService<Condition> conditionService;

    private readonly IFeatureService<Color> colorService;
    private readonly IFeatureService<Brand> brandService;

    private readonly IFeatureService<Site> siteService;
    private readonly ISearchFilterBuilder searchFilterBuilder;

    public AdvertService(
        IMapper mapper,
        IEuroStandardService euroStandardService,
        IPopulatedPlaceService populatedPlaceService,
        IRepository<Advert> advertRepository,
        IRepository<Image> imageRepository,
        IFeatureService<Transmission> transmissionService,
        IFeatureService<BodyStyle> bodyStyleService,
        IFeatureService<Engine> engineService,
        IFeatureService<Condition> conditionService,
        IFeatureService<Color> colorService,
        IFeatureService<Brand> brandService,
        IFeatureService<Site> siteService,
        ISearchFilterBuilder advertSearchFilterBuilder)
    {
        this.mapper = mapper;
        this.advertRepository = advertRepository;
        this.imageRepository = imageRepository;
        this.transmissionService = transmissionService;
        this.bodyStyleService = bodyStyleService;
        this.engineService = engineService;
        this.conditionService = conditionService;
        this.colorService = colorService;
        this.euroStandardService = euroStandardService;
        this.populatedPlaceService = populatedPlaceService;
        this.brandService = brandService;
        this.siteService = siteService;
        this.searchFilterBuilder = advertSearchFilterBuilder;
    }

    public async Task AddOrUpdate(NormalizedAdvert normalizedAdvert)
    {
        var siteId = siteService.FindIdByName(normalizedAdvert.Site);
        var transmissionId = transmissionService.FindIdByName(normalizedAdvert.Transmission);

        var engineId = engineService.FindIdByName(normalizedAdvert.Engine);
        var bodyStyleId = bodyStyleService.FindIdByName(normalizedAdvert.BodyStyle);

        var colorId = colorService.FindIdByName(normalizedAdvert.Color);
        var conditionId = conditionService.FindIdByName(normalizedAdvert.Condition);

        var brand = brandService.FindByName(normalizedAdvert.Brand);
        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        var (euroStandard, isEuroStandardApproximate) = await FindOrApproximateEuroStandard(
            normalizedAdvert.EuroStandard,
            normalizedAdvert.ManufacturedOn);

        var populatedPlace = populatedPlaceService.FindByRegion(
            normalizedAdvert.Region,
            normalizedAdvert.PopulatedPlace,
            normalizedAdvert.PopulatedPlaceType);

        var images = normalizedAdvert.ImageUrls
            .Select(url => new Image { Url = url }).ToList();

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
            await advertRepository.AddAsync(advert);
        }

        await advertRepository.SaveChangesAsync();
    }

    public async Task<GetFullAdvertResultModel?> GetFullAdvert(string id)
    {
        var advert = await advertRepository.All().FirstOrDefaultAsync(a => a.Id == id);
        return mapper.Map<GetFullAdvertResultModel>(advert);
    }

    public DateTime? FindLatestAdvertModifiedOnDate(string site)
        => FindAdvertsBySiteId(siteService.FindIdByName(site))
            .FirstOrDefault()?
            .ModifiedOn;

    public IEnumerable<SearchAdvertsResultModel> SearchAdverts(
        SearchAdvertsInputModel serviceModel,
        int pageIndex,
        int resultsPerPageCount)
        => FilterAdvertsBy(serviceModel)
            .Skip(count: pageIndex * resultsPerPageCount)
            .Take(resultsPerPageCount)
            .ToList()
            .Select(mapper.Map<Advert, SearchAdvertsResultModel>);

    private IQueryable<Advert> FilterAdvertsBy(SearchAdvertsInputModel input)
        => searchFilterBuilder
            .CreateFilterFor(advertRepository.All())
            .ByBodyStyle(input.BodyStyle)
            .ByMaking(input.Brand, input.ModelId)
            .ByColor(input.Color)
            .ByCondition(input.Condition)
            .ByEuroStandard(input.EuroStandard)
            .ByEngine(input.Engine)
            .ByLocation(input.Region, input.PopulatedPlaceId)
            .ByTransmission(input.Transmission)
            .ByPower(input.MinPowerInHp, input.MaxPowerInHp)
            .ByKilometrage(input.MinMileageInKm, input.MaxMileageInKm)
            .ByYear(input.MinYear, input.MaxYear)
            .ByPrice(input.MinPriceInBgn, input.MaxPriceInBgn)
            .ApplyFilter();

    private IQueryable<Advert> FindAdvertsBySiteId(int? siteId)
        => advertRepository.All().Where(a => a.SiteId == siteId);

    private async Task<(Advert advert, bool doesAdvertExist)> FindAdvertBySiteInfo(string? remoteId, int? siteId)
    {
        var advert = await advertRepository.All()
            .FirstOrDefaultAsync(a => a.RemoteId == remoteId && a.SiteId == siteId) ?? new Advert();

        bool doesAdvertExist = advertRepository.All()
            .Any(a => a.RemoteId == remoteId && a.SiteId == siteId);

        return (advert, doesAdvertExist);
    }

    private async Task<(EuroStandard? euroStandard, bool isApproximate)> FindOrApproximateEuroStandard(
        string? euroStandardName,
        DateTime? manufacturedOn)
    {
        bool isApproximate = false;
        var euroStandard = euroStandardService.FindByName(euroStandardName);

        if (euroStandard == null)
        {
            isApproximate = true;
            euroStandard = await euroStandardService.Approximate(manufacturedOn);
        }

        return (euroStandard, isApproximate);
    }

    private void DeleteAdvertImages(IList<Image> imagesToDelete)
    {
        foreach (var imageToDelete in imagesToDelete)
        {
            imageRepository.Delete(imageToDelete);
        }
    }
}
