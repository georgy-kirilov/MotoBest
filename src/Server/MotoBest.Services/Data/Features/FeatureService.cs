using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MotoBest.Data.Models.Common;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class FeatureService<TFeature> : IFeatureService<TFeature>
    where TFeature : Feature, new()
{
    protected readonly IRepository<TFeature> featureRepository;
    protected readonly IMapper mapper;

    public FeatureService(IRepository<TFeature> featureRepository, IMapper mapper)
    {
        this.featureRepository = featureRepository;
        this.mapper = mapper;
    }

    public int? FindIdByName(string? name) => FindByName(name)?.Id;

    public TFeature? FindByName(string? name)
        => featureRepository
        .All()
        .FirstOrDefault(feat => feat.Name == name);

    public IEnumerable<FeatureResultModel> GetAll()
        => featureRepository
        .All()
        .ToList()
        .Select(mapper.Map<FeatureResultModel>);

    public async Task<TFeature?> FindById(int? id)
        => await featureRepository
        .All()
        .FirstOrDefaultAsync(feat => feat.Id == id);
}
