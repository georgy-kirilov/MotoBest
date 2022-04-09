using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data;

public class AdvertService : IAdvertService
{
    private readonly IRepository<Advert> advertRepository;
    private readonly IRepository<Transmission> transmissionRepository;
    private readonly IRepository<BodyStyle> bodyStyleRepository;
    private readonly IRepository<Engine> engineRepository;
    private readonly IRepository<Condition> conditionRepository;
    private readonly IRepository<Color> colorRepository;
    private readonly IRepository<Region> regionRepository;
    private readonly IRepository<EuroStandard> euroStandardRepository;
    private readonly IRepository<PopulatedPlace> populatedPlaceRepository;
    private readonly IRepository<Brand> brandRepository;
    private readonly IRepository<Site> siteRepository;

    public AdvertService(
        IRepository<Advert> advertRepository,
        IRepository<Transmission> transmissionRepository,
        IRepository<BodyStyle> bodyStyleRepository,
        IRepository<Engine> engineRepository,
        IRepository<Condition> conditionRepository,
        IRepository<Color> colorRepository,
        IRepository<Region> regionRepository,
        IRepository<EuroStandard> euroStandardRepository,
        IRepository<PopulatedPlace> populatedPlaceRepository,
        IRepository<Brand> brandRepository,
        IRepository<Site> siteRepository)
    {
        this.advertRepository = advertRepository;
        this.transmissionRepository = transmissionRepository;
        this.bodyStyleRepository = bodyStyleRepository;
        this.engineRepository = engineRepository;
        this.conditionRepository = conditionRepository;
        this.colorRepository = colorRepository;
        this.regionRepository = regionRepository;
        this.euroStandardRepository = euroStandardRepository;
        this.populatedPlaceRepository = populatedPlaceRepository;
        this.brandRepository = brandRepository;
        this.siteRepository = siteRepository;
    }

    public async Task AddAsync(NormalizedAdvert normalizedAdvert)
    {
        var site = await siteRepository.All()
            .FirstOrDefaultAsync(s => s.Name == normalizedAdvert.Site);

        var transmission = await transmissionRepository.All()
            .FirstOrDefaultAsync(t => t.Name == normalizedAdvert.Transmission);

        var bodyStyle = await bodyStyleRepository.All()
            .FirstOrDefaultAsync(bs => bs.Name == normalizedAdvert.BodyStyle);

        var engine = await engineRepository.All()
            .FirstOrDefaultAsync(e => e.Name == normalizedAdvert.Engine);

        var condition = await conditionRepository.All()
            .FirstOrDefaultAsync(c => c.Name == normalizedAdvert.Condition);

        var color = await colorRepository.All()
            .FirstOrDefaultAsync(c => c.Name == normalizedAdvert.Color);

        var euroStandard = await euroStandardRepository.All()
            .FirstOrDefaultAsync(es => es.Name == normalizedAdvert.EuroStandard);

        bool isEuroStandardApproximate = false;

        if (euroStandard == null)
        {
            isEuroStandardApproximate = true;

            euroStandard = await euroStandardRepository.All()
                .Where(es => es.FromDate > normalizedAdvert.ManufacturedOn)
                .OrderByDescending(es => es.FromDate)
                .FirstOrDefaultAsync();
        }

        var region = await regionRepository.All()
            .FirstOrDefaultAsync(r => r.Name == normalizedAdvert.Region);

        var populatedPlaceSource = region?.PopulatedPlaces.AsQueryable() ?? populatedPlaceRepository.All();

        var populatedPlace = populatedPlaceSource.FirstOrDefault(pp =>
            pp.Name == normalizedAdvert.PopulatedPlace && pp.Type == normalizedAdvert.PopulatedPlaceType);

        region ??= populatedPlace?.Region;

        var brand = await brandRepository.All()
            .FirstOrDefaultAsync(b => b.Name == normalizedAdvert.Brand);

        var model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        await advertRepository.AddAsync(new Advert
        {
            SiteId = site?.Id,
            RemoteId = normalizedAdvert.RemoteId,
            Title = normalizedAdvert.Title,
            Description = normalizedAdvert.Description,
            PriceBgn = normalizedAdvert.PriceBgn,
            ManufacturedOn = normalizedAdvert.ManufacturedOn,
            HorsePowers = normalizedAdvert.HorsePowers,
            Kilometrage = normalizedAdvert.Kilometrage,
            TransmissionId = transmission?.Id,
            BodyStyleId = bodyStyle?.Id,
            EngineId = engine?.Id,
            ConditionId = condition?.Id,
            ColorId = color?.Id,
            RegionId = region?.Id,
            IsEuroStandardApproximate = isEuroStandardApproximate,
            EuroStandardId = euroStandard?.Id,
            PopulatedPlaceId = populatedPlace?.Id,
            BrandId = brand?.Id,
            ModelId = model?.Id,
        });

        await advertRepository.SaveChangesAsync();
    }
}
