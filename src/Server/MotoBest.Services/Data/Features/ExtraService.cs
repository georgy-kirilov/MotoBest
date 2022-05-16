using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class ExtraService : FeatureService<Extra>, IExtraService
{
    public ExtraService(
        IRepository<Extra> featureRepository,
        IMapper mapper)
        : base(featureRepository, mapper)
    {
    }

    public IEnumerable<FeatureResultModel> GetAllByType(int? typeId)
        => featureRepository.All()
        .Where(x => x.TypeId == typeId)
        .Select(mapper.Map<FeatureResultModel>)
        .ToList();
}
