using MotoBest.Data.Models;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public interface IExtraService : IFeatureService<Extra>
{
    IEnumerable<FeatureResultModel> GetAllByType(int? typeId);
}
