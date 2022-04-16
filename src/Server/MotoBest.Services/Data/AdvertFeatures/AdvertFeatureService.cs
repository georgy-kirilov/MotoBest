using Microsoft.EntityFrameworkCore;

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

    public async Task<int?> FindIdByNameAsync(string? name)
        => (await FindByNameAsync(name))?.Id;

    public async Task<TFeature?> FindByNameAsync(string? name)
        => await featureRepository.All().FirstOrDefaultAsync(f => f.Name == name);
}
