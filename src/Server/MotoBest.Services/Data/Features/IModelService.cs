using MotoBest.Data.Models;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public interface IModelService : IFeatureService<Model>
{
    IEnumerable<FeatureResultModel> FindAllByBrand(string? brand);
}
