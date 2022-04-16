using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.AdvertFeatures;

public class PopulatedPlaceService : AdvertFeatureService<PopulatedPlace>, IPopulatedPlaceService
{
    private readonly IAdvertFeatureService<Region> regionService;

    public PopulatedPlaceService(
        IRepository<PopulatedPlace> featureRepository,
        IAdvertFeatureService<Region> regionService)
        : base(featureRepository)
    {
        this.regionService = regionService;
    }

    public async Task<PopulatedPlace?> FindByRegionAsync(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType)
    {
        var region = await regionService.FindByNameAsync(regionName);
        var source = region?.PopulatedPlaces.AsQueryable() ?? featureRepository.All();
        return source.FirstOrDefault(pp => pp.Name == populatedPlaceName && pp.Type == populatedPlaceType);
    }
}
