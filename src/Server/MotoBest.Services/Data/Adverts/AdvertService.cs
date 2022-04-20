using AutoMapper;

using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.Services.Data.AdvertFeatures;
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

    private readonly IAdvertFeatureService<Transmission> transmissionService;
    private readonly IAdvertFeatureService<BodyStyle> bodyStyleService;

    private readonly IAdvertFeatureService<Engine> engineService;
    private readonly IAdvertFeatureService<Condition> conditionService;

    private readonly IAdvertFeatureService<Color> colorService;
    private readonly IAdvertFeatureService<Brand> brandService;

    private readonly IAdvertFeatureService<Site> siteService;
    private readonly IAdvertSearchFilterBuilder advertSearchFilterBuilder;

    public AdvertService(
        IMapper mapper,
        IEuroStandardService euroStandardService,
        IPopulatedPlaceService populatedPlaceService,
        IRepository<Advert> advertRepository,
        IRepository<Image> imageRepository,
        IAdvertFeatureService<Transmission> transmissionService,
        IAdvertFeatureService<BodyStyle> bodyStyleService,
        IAdvertFeatureService<Engine> engineService,
        IAdvertFeatureService<Condition> conditionService,
        IAdvertFeatureService<Color> colorService,
        IAdvertFeatureService<Brand> brandService,
        IAdvertFeatureService<Site> siteService,
        IAdvertSearchFilterBuilder advertSearchFilterBuilder)
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
        this.advertSearchFilterBuilder = advertSearchFilterBuilder;
    }

    public async Task AddOrUpdateAsync(NormalizedAdvert normalizedAdvert)
    {
        var siteId = siteService.FindIdByName(normalizedAdvert.Site);
        var transmissionId = transmissionService.FindIdByName(normalizedAdvert.Transmission);

        var engineId = engineService.FindIdByName(normalizedAdvert.Engine);
        var bodyStyleId = bodyStyleService.FindIdByName(normalizedAdvert.BodyStyle);

        var colorId = colorService.FindIdByName(normalizedAdvert.Color);
        var conditionId = conditionService.FindIdByName(normalizedAdvert.Condition);

        var brand = brandService.FindByName(normalizedAdvert.Brand);
        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        var (euroStandard, isEuroStandardApproximate) = await FindEuroStandardAsync(
            normalizedAdvert.EuroStandard,
            normalizedAdvert.ManufacturedOn);

        var populatedPlace = populatedPlaceService.FindByRegion(
            normalizedAdvert.Region,
            normalizedAdvert.PopulatedPlace,
            normalizedAdvert.PopulatedPlaceType);

        var images = normalizedAdvert.ImageUrls
            .Select(url => new Image { Url = url }).ToList();

        var (advert, doesAdvertExist) = await FindAdvertBySiteInfoAsync(normalizedAdvert.RemoteId, siteId);

        DeleteAdvertImages(advert.Images.ToList());

        advert.Images = images;

        advert.SiteId = siteId;
        advert.RemoteId = normalizedAdvert.RemoteId;

        advert.Title = normalizedAdvert.Title;
        advert.Description = normalizedAdvert.Description;

        advert.ColorId = colorId;
        advert.ConditionId = conditionId;

        advert.EngineId = engineId;
        advert.PriceBgn = normalizedAdvert.PriceBgn;

        advert.BodyStyleId = bodyStyleId;
        advert.TransmissionId = transmissionId;

        advert.ModifiedOn = normalizedAdvert.ModifiedOn;
        advert.ManufacturedOn = normalizedAdvert.ManufacturedOn;

        advert.HorsePowers = normalizedAdvert.HorsePowers;
        advert.Kilometrage = normalizedAdvert.Kilometrage;

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

    public async Task<FullAdvertDto?> GetFullAdvertAsync(string id)
    {
        var advert = await advertRepository.All().FirstOrDefaultAsync(a => a.Id == id);
        return mapper.Map<FullAdvertDto>(advert);
    }

    public DateTime? FindLatestAdvertModifiedOnDate(string site)
        => FindAdvertsBySiteId(siteService.FindIdByName(site))
            .FirstOrDefault()?
            .ModifiedOn;

    public IEnumerable<AdvertSearchResultDto> SearchAdverts(
        AdvertSearchFilterDto filter,
        int pageIndex,
        int resultsPerPageCount)
        => FilterAdvertsBy(filter)
            .Skip(count: pageIndex * resultsPerPageCount)
            .Take(resultsPerPageCount)
            .ToList()
            .Select(mapper.Map<Advert, AdvertSearchResultDto>);

    private IQueryable<Advert> FilterAdvertsBy(AdvertSearchFilterDto filter)
        => advertSearchFilterBuilder
            .CreateFilterFor(advertRepository.All())
            .ByBodyStyle(filter.BodyStyle)
            .ByBrand(filter.Brand)
            .ByColor(filter.Color)
            .ByCondition(filter.Condition)
            .ByEngine(filter.Engine)
            .ByRegion(filter.Region)
            .ByTransmission(filter.Transmission)
            .ByHorsePowers(filter.MinHorsePowers, filter.MaxHorsePowers)
            .ByKilometrage(filter.MinKilometrage, filter.MaxKilometrage)
            .ByYear(filter.MinYear, filter.MaxYear)
            .ApplyFilter();

    private IQueryable<Advert> FindAdvertsBySiteId(int? siteId)
        => advertRepository.All().Where(a => a.SiteId == siteId);

    private async Task<(Advert advert, bool doesAdvertExist)> FindAdvertBySiteInfoAsync(string? remoteId, int? siteId)
    {
        var advert = await advertRepository.All()
            .FirstOrDefaultAsync(a => a.RemoteId == remoteId && a.SiteId == siteId) ?? new Advert();

        bool doesAdvertExist = advertRepository.All()
            .Any(a => a.RemoteId == remoteId && a.SiteId == siteId);

        return (advert, doesAdvertExist);
    }

    private async Task<(EuroStandard? euroStandard, bool isApproximate)> FindEuroStandardAsync(
        string? euroStandardName, DateTime? manufacturedOn)
    {
        bool isApproximate = false;
        var euroStandard = euroStandardService.FindByName(euroStandardName);

        if (euroStandard == null)
        {
            isApproximate = true;
            euroStandard = await euroStandardService.ApproximateAsync(manufacturedOn);
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
