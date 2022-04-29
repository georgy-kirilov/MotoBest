using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Features;

public interface IPopulatedPlaceService : IFeatureService<PopulatedPlace>
{
    PopulatedPlace? FindByRegion(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType);
}
