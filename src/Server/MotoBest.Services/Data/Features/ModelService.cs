using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Data.Features;

public class ModelService : FeatureService<Model>, IModelService
{
    private readonly IFeatureService<Brand> brandService;
    private readonly IMapper mapper;

    public ModelService(
        IRepository<Model> featureRepository,
        IFeatureService<Brand> brandService,
        IMapper mapper)
        : base(featureRepository)
    {
        this.brandService = brandService;
        this.mapper = mapper;
    }

    public IEnumerable<GetAllModelsByBrandResultModel> FindAllByBrand(string? brand)
    {
        var models = brandService
            .FindByName(brand)?
            .Models
            .Select(mapper.Map<GetAllModelsByBrandResultModel>);

        return models ?? new List<GetAllModelsByBrandResultModel>();
    }
}
