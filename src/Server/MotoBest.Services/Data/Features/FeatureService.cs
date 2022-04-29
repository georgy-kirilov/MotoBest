using MotoBest.Data.Models.Common;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.Features;

public class FeatureService<TFeature> : IFeatureService<TFeature>
    where TFeature : Feature, new()
{
    protected readonly IRepository<TFeature> featureRepository;

    public FeatureService(IRepository<TFeature> featureRepository)
    {
        this.featureRepository = featureRepository;
    }

    public int? FindIdByName(string? name) => FindByName(name)?.Id;

    public TFeature? FindByName(string? name)
        => featureRepository.All().FirstOrDefault(f => f.Name == name);

    public IEnumerable<string> GetAllNames()
        => featureRepository.All().Select(f => f.Name);
}
