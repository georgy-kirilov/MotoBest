using MotoBest.Data.Models.Common;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public interface IFeatureService<TFeature>
    where TFeature : Feature, new()
{
    /// <summary>
    /// Returns the id of the model with the given name or null if such is not found
    /// </summary>
    int? FindIdByName(string? name);

    /// <summary>
    /// Returns the model with the given name or null if such is not found
    /// </summary>
    TFeature? FindByName(string? name);

    IEnumerable<FeatureResultModel> GetAll();
}
