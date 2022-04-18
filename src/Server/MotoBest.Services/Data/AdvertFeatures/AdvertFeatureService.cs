using MotoBest.Data.Models.Common;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.AdvertFeatures;

public class AdvertFeatureService<TFeature> : IAdvertFeatureService<TFeature>
    where TFeature : AdvertFeature, new()
{
    protected readonly IRepository<TFeature> featureRepository;

    public AdvertFeatureService(IRepository<TFeature> featureRepository)
    {
        this.featureRepository = featureRepository;
    }

    public int? FindIdByName(string? name) => FindByName(name)?.Id;

    public TFeature? FindByName(string? name)
        => featureRepository.All().FirstOrDefault(f => f.Name == name);
}
