using MotoBest.Data.Models;

namespace MotoBest.Services.Data.AdvertFeatures;

public interface IPopulatedPlaceService : IAdvertFeatureService<PopulatedPlace>
{
    Task<PopulatedPlace?> FindByRegionAsync(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType);
}
