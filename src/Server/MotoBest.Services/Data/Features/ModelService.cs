using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class ModelService : FeatureService<Model>, IModelService
{
    private readonly IFeatureService<Brand> brandService;

    public ModelService(
        IRepository<Model> featureRepository,
        IFeatureService<Brand> brandService,
        IMapper mapper)
        : base(featureRepository, mapper)
    {
        this.brandService = brandService;
    }

    public IEnumerable<FeatureResultModel> FindAllByBrand(string? brand)
    {
        var models = brandService
            .FindByName(brand)?
            .Models
            .Select(mapper.Map<FeatureResultModel>);

        return models ?? new List<FeatureResultModel>();
    }
}
