using MotoBest.Data.Models;

namespace MotoBest.Services.Data.AdvertFeatures;

public interface IPopulatedPlaceService : IAdvertFeatureService<PopulatedPlace>
{
    PopulatedPlace? FindByRegion(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType);
}
