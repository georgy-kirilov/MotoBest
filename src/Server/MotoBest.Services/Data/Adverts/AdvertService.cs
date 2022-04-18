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
    private readonly IRepository<Advert> advertRepository;

    private readonly IAdvertFeatureService<Transmission> transmissionService;
    private readonly IAdvertFeatureService<BodyStyle> bodyStyleService;
    private readonly IAdvertFeatureService<Engine> engineService;

    private readonly IAdvertFeatureService<Condition> conditionService;
    private readonly IAdvertFeatureService<Color> colorService;
    private readonly IEuroStandardService euroStandardService;

    private readonly IPopulatedPlaceService populatedPlaceService;
    private readonly IAdvertFeatureService<Brand> brandService;
    private readonly IAdvertFeatureService<Site> siteService;
    private readonly IAdvertSearchFilterBuilder searchAdvertsFilterBuilder;

    public AdvertService(
        IRepository<Advert> advertRepository,
        IAdvertFeatureService<Transmission> transmissionService,
        IAdvertFeatureService<BodyStyle> bodyStyleService,
        IAdvertFeatureService<Engine> engineService,
        IAdvertFeatureService<Condition> conditionService,
        IAdvertFeatureService<Color> colorService,
        IEuroStandardService euroStandardService,
        IPopulatedPlaceService populatedPlaceService,
        IAdvertFeatureService<Brand> brandService,
        IAdvertFeatureService<Site> siteService,
        IAdvertSearchFilterBuilder searchAdvertsFilterBuilder,
        IMapper mapper)
    {
        this.advertRepository = advertRepository;
        this.transmissionService = transmissionService;
        this.bodyStyleService = bodyStyleService;
        this.engineService = engineService;
        this.conditionService = conditionService;
        this.colorService = colorService;
        this.euroStandardService = euroStandardService;
        this.populatedPlaceService = populatedPlaceService;
        this.brandService = brandService;
        this.siteService = siteService;
        this.searchAdvertsFilterBuilder = searchAdvertsFilterBuilder;
        this.mapper = mapper;
    }

    public async Task AddOrUpdateAsync(NormalizedAdvert normalizedAdvert)
    {
        var siteId = siteService.FindIdByName(normalizedAdvert.Site);
        var transmissionId = transmissionService.FindIdByName(normalizedAdvert.Transmission);

        var bodyStyleId = bodyStyleService.FindIdByName(normalizedAdvert.BodyStyle);
        var engineId = engineService.FindIdByName(normalizedAdvert.Engine);

        var conditionId = conditionService.FindIdByName(normalizedAdvert.Condition);
        var colorId = colorService.FindIdByName(normalizedAdvert.Color);

        var brand = brandService.FindByName(normalizedAdvert.Brand);
        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        var populatedPlace = populatedPlaceService.FindByRegion(
            normalizedAdvert.Region, normalizedAdvert.PopulatedPlace, normalizedAdvert.PopulatedPlaceType);

        bool isEuroStandardApproximate = false;
        var euroStandard = euroStandardService.FindByName(normalizedAdvert.EuroStandard);

        if (euroStandard == null)
        {
            isEuroStandardApproximate = true;
            euroStandard = await euroStandardService.ApproximateAsync(normalizedAdvert.ManufacturedOn);
        }

        var images = normalizedAdvert.ImageUrls
            .Select(url => new Image { Url = url })
            .ToList();

        var advert = await advertRepository.All()
            .FirstOrDefaultAsync(a => a.RemoteId == normalizedAdvert.RemoteId);

        bool doesAdvertExist = advert != null;

        if (advert == null)
        {
            advert = new Advert();
        }

        advert.SiteId = siteId;
        advert.RemoteId = normalizedAdvert.RemoteId;
        advert.Title = normalizedAdvert.Title;
        advert.Description = normalizedAdvert.Description;
        advert.PriceBgn = normalizedAdvert.PriceBgn;
        advert.ManufacturedOn = normalizedAdvert.ManufacturedOn;
        advert.ModifiedOn = normalizedAdvert.ModifiedOn;
        advert.HorsePowers = normalizedAdvert.HorsePowers;
        advert.Kilometrage = normalizedAdvert.Kilometrage;
        advert.TransmissionId = transmissionId;
        advert.BodyStyleId = bodyStyleId;
        advert.EngineId = engineId;
        advert.ConditionId = conditionId;
        advert.ColorId = colorId;
        advert.RegionId = populatedPlace?.RegionId;
        advert.IsEuroStandardApproximate = isEuroStandardApproximate;
        advert.EuroStandardId = euroStandard?.Id;
        advert.PopulatedPlaceId = populatedPlace?.Id;
        advert.BrandId = brand?.Id;
        advert.ModelId = model?.Id;
        advert.Images = images;
        
        if (!doesAdvertExist)
        {
            await advertRepository.AddAsync(advert);
        }

        await advertRepository.SaveChangesAsync();
    }

    public async Task<FullAdvertDto?> GetFullAdvertAsync(string id)
    {
        var advert = await advertRepository.All().FirstOrDefaultAsync(a => a.Id == id);

        if (advert == null)
        {
            return null;
        }

        return mapper.Map<FullAdvertDto>(advert);
    }

    public DateTime? GetLatestAdvertModifiedOnDate(string site)
    {
        var siteId = siteService.FindIdByName(site);

        return advertRepository.All()
            .Where(a => a.SiteId == siteId)
            .FirstOrDefault()?
            .ModifiedOn;
    }

    public IEnumerable<AdvertSearchResultDto> SearchAdverts(AdvertSearchFilterDto filter, int pageIndex, int resultsPerPageCount)
        => FilterAdvertsBy(filter)
            .Skip(count: pageIndex * resultsPerPageCount)
            .Take(resultsPerPageCount)
            .ToList()
            .Select(mapper.Map<Advert, AdvertSearchResultDto>);

    private IQueryable<Advert> FilterAdvertsBy(AdvertSearchFilterDto filter)
        => searchAdvertsFilterBuilder
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
}
