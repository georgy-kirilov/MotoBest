using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;
using MotoBest.Services.Data.AdvertFeatures;
using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data.Adverts;

public class AdvertService : IAdvertService
{
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
        IAdvertFeatureService<Site> siteService)
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
    }

    public async Task AddAsync(NormalizedAdvert normalizedAdvert)
    {
        var siteId = await siteService.FindIdByNameAsync(normalizedAdvert.Site);
        var transmissionId = await transmissionService.FindIdByNameAsync(normalizedAdvert.Transmission);
        var bodyStyleId = await bodyStyleService.FindIdByNameAsync(normalizedAdvert.BodyStyle);
        var engineId = await engineService.FindIdByNameAsync(normalizedAdvert.Engine);
        var conditionId = await conditionService.FindIdByNameAsync(normalizedAdvert.Condition);
        var colorId = await colorService.FindIdByNameAsync(normalizedAdvert.Color);

        var brand = await brandService.FindByNameAsync(normalizedAdvert.Brand);
        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        var populatedPlace = await populatedPlaceService.FindByRegionAsync(
            normalizedAdvert.Region, normalizedAdvert.PopulatedPlace, normalizedAdvert.PopulatedPlaceType);

        bool isEuroStandardApproximate = false;
        var euroStandard = await euroStandardService.FindByNameAsync(normalizedAdvert.EuroStandard);

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

    public async Task<DateTime?> LatestAdvertModifiedOnDateAsync(string site)
    {
        var siteId = await siteService.FindIdByNameAsync(site);

        return advertRepository.All()
            .Where(a => a.SiteId == siteId)
            .FirstOrDefault()?
            .ModifiedOn;
    }

    public async Task<IEnumerable<SearchAdvertResult>> SearchAdvertsAsync(SearchAdvertsFilter filter, int pageIndex, int resultsPerPageCount)
        => (await ApplyFilterToAdvertsAsync(filter))
            .Skip(pageIndex * resultsPerPageCount)
            .Take(resultsPerPageCount)
            .ToList()
            .Select(a => new SearchAdvertResult
            {
                Title = a.Title,
                ModifiedOn = a.ModifiedOn,
                HorsePowers = a.HorsePowers,
                Kilometrage = a.Kilometrage,
                MainImageUrl = a.Images.FirstOrDefault()?.Url ?? "default-image",
                Month = a.ManufacturedOn?.Month.ToString(),
                Year = a.ManufacturedOn?.Year,
                Price = a.PriceBgn,
                Transmission = a.Transmission?.Name,
                Engine = a.Engine?.Name,
            });

    private async Task<IQueryable<Advert>> ApplyFilterToAdvertsAsync(SearchAdvertsFilter filter)
    {
        var engineId = await engineService.FindIdByNameAsync(filter.Engine);
        var transmissionId = await transmissionService.FindIdByNameAsync(filter.Transmission);
        var colorId = await colorService.FindIdByNameAsync(filter.Color);
        var conditionId = await conditionService.FindIdByNameAsync(filter.Condition);
        var brandId = await brandService.FindIdByNameAsync(filter.Brand);

        return advertRepository.All()
            .Where(a => (filter.Engine == null || a.EngineId == engineId)
                && (filter.Transmission == null || a.TransmissionId == transmissionId)
                && (filter.Color == null || a.ColorId == colorId)
                && (filter.Condition == null || a.ConditionId == conditionId)
                && (filter.Brand == null || a.BrandId == brandId));
    }
}
