using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;
using MotoBest.Services.Normalizing;

namespace MotoBest.Services.Data;

public class AdvertsService : IAdvertsService
{
    private readonly IRepository<Advert> advertsRepository;
    private readonly IRepository<Transmission> transmissionsRepository;
    private readonly IRepository<BodyStyle> bodyStylesRepository;
    private readonly IRepository<Engine> enginesRepository;
    private readonly IRepository<Condition> conditionsRepository;
    private readonly IRepository<Color> colorsRepository;
    private readonly IRepository<Region> regionsRepository;
    private readonly IRepository<EuroStandard> euroStandardsRepository;
    private readonly IRepository<PopulatedPlace> townsRepository;
    private readonly IRepository<Brand> brandsRepository;

    public AdvertsService(
        IRepository<Advert> advertsRepository,
        IRepository<Transmission> transmissionsRepository,
        IRepository<BodyStyle> bodyStylesRepository,
        IRepository<Engine> enginesRepository,
        IRepository<Condition> conditionsRepository,
        IRepository<Color> colorsRepository,
        IRepository<Region> regionsRepository,
        IRepository<EuroStandard> euroStandardsRepository,
        IRepository<PopulatedPlace> townsRepository,
        IRepository<Brand> brandsRepository)
    {
        this.advertsRepository = advertsRepository;
        this.transmissionsRepository = transmissionsRepository;
        this.bodyStylesRepository = bodyStylesRepository;
        this.enginesRepository = enginesRepository;
        this.conditionsRepository = conditionsRepository;
        this.colorsRepository = colorsRepository;
        this.regionsRepository = regionsRepository;
        this.euroStandardsRepository = euroStandardsRepository;
        this.townsRepository = townsRepository;
        this.brandsRepository = brandsRepository;
    }

    public async Task AddAsync(NormalizedAdvert normalizedAdvert)
    {
        Transmission? transmission = await transmissionsRepository.All()
            .FirstOrDefaultAsync(t => t.Name == normalizedAdvert.Transmission);

        BodyStyle? bodyStyle = await bodyStylesRepository.All()
            .FirstOrDefaultAsync(bs => bs.Name == normalizedAdvert.BodyStyle);

        Engine? engine = await enginesRepository.All()
            .FirstOrDefaultAsync(e => e.Name == normalizedAdvert.Engine);

        Condition? condition = await conditionsRepository.All()
            .FirstOrDefaultAsync(c => c.Name == normalizedAdvert.Condition);

        Color? color = await colorsRepository.All()
            .FirstOrDefaultAsync(c => c.Name == normalizedAdvert.Color);

        EuroStandard? euroStandard = await euroStandardsRepository.All()
            .FirstOrDefaultAsync(es => es.Name == normalizedAdvert.EuroStandard);

        bool isEuroStandardApproximate = false;

        if (euroStandard == null)
        {
            isEuroStandardApproximate = true;

            euroStandard = await euroStandardsRepository.All()
                .Where(es => es.FromDate > normalizedAdvert.ManufacturedOn)
                .OrderByDescending(es => es.FromDate)
                .FirstOrDefaultAsync();
        }

        Region? region = await regionsRepository.All()
            .FirstOrDefaultAsync(r => r.Name == normalizedAdvert.Region);

        PopulatedPlace? town = await townsRepository.All()
            .FirstOrDefaultAsync(t => t.Name == normalizedAdvert.PopulatedPlace);

        Brand? brand = await brandsRepository.All()
            .FirstOrDefaultAsync(b => b.Name == normalizedAdvert.Brand);

        Model? model = brand?.Models.FirstOrDefault(m => m.Name == normalizedAdvert.Model);

        await advertsRepository.AddAsync(new Advert
        {
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
            RegionId = region?.Id ?? town?.RegionId,
            IsEuroStandardApproximate = isEuroStandardApproximate,
            EuroStandardId = euroStandard?.Id,
            PopulatedPlaceId = town?.Id,
            BrandId = brand?.Id,
            ModelId = model?.Id,
            RemoteId = normalizedAdvert.RemoteId,
        });

        await advertsRepository.SaveChangesAsync();
    }
}
