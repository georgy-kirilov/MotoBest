using MotoBest.Data.Models;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public interface IPopulatedPlaceService : IFeatureService<PopulatedPlace>
{
    PopulatedPlace? FindByRegion(
        string? regionName,
        string? populatedPlaceName,
        PopulatedPlaceType? populatedPlaceType);

    IEnumerable<GetAllPopulatedPlacesByRegionResultModel> FindAllByRegion(string? regionName);
}
