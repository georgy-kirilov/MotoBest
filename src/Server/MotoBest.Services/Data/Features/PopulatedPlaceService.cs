using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class PopulatedPlaceService : FeatureService<PopulatedPlace>, IPopulatedPlaceService
{
    private readonly IFeatureService<Region> regions;

    public PopulatedPlaceService(
        IRepository<PopulatedPlace> features,
        IFeatureService<Region> regions,
        IMapper mapper)
        : base(features, mapper)
    {
        this.regions = regions;
    }

    public PopulatedPlace? FindByRegion(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType)
    {
        var region = regions.FindByName(regionName);
        var source = region?.PopulatedPlaces.AsQueryable() ?? featureRepository.All();
        return source.FirstOrDefault(pp => pp.Name == populatedPlaceName && pp.Type == populatedPlaceType);
    }

    public async Task<IEnumerable<FeatureResultModel>> GetAllByRegion(int? regionId)
    {
        var region = await regions.FindById(regionId);
        var populatedPlaces = region?.PopulatedPlaces.Select(mapper.Map<FeatureResultModel>);
        return populatedPlaces ?? new List<FeatureResultModel>();
    }
}
