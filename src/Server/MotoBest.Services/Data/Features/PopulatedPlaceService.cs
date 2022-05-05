using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class PopulatedPlaceService : FeatureService<PopulatedPlace>, IPopulatedPlaceService
{
    private readonly IFeatureService<Region> regionService;

    public PopulatedPlaceService(
        IRepository<PopulatedPlace> featureRepository,
        IFeatureService<Region> regionService,
        IMapper mapper)
        : base(featureRepository, mapper)
    {
        this.regionService = regionService;
    }

    public PopulatedPlace? FindByRegion(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType)
    {
        var region = regionService.FindByName(regionName);
        var source = region?.PopulatedPlaces.AsQueryable() ?? featureRepository.All();
        return source.FirstOrDefault(pp => pp.Name == populatedPlaceName && pp.Type == populatedPlaceType);
    }

    public IEnumerable<FeatureResultModel> FindAllByRegion(string? regionName)
    {
        var populatedPlaces = regionService
            .FindByName(regionName)?
            .PopulatedPlaces
            .Select(mapper.Map<FeatureResultModel>);

        return populatedPlaces ?? new List<FeatureResultModel>();
    }
}
