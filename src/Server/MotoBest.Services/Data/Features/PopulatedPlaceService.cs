using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.AdvertFeatures;

public class PopulatedPlaceService : FeatureService<PopulatedPlace>, IPopulatedPlaceService
{
    private readonly IFeatureService<Region> regionService;

    public PopulatedPlaceService(
        IRepository<PopulatedPlace> featureRepository,
        IFeatureService<Region> regionService)
        : base(featureRepository)
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
}
