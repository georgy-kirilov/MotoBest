using Microsoft.AspNetCore.Mvc;

using MotoBest.Data.Models;
using MotoBest.Services.Data.Features;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.WebApi.Controllers;

public class FeaturesController : ApiController
{
    private readonly IFeatureService<Transmission> transmissionService;
    private readonly IFeatureService<Engine> engineService;
    private readonly IFeatureService<BodyStyle> bodyStyleService;
    private readonly IFeatureService<Brand> brandService;
    private readonly IFeatureService<Region> regionService;
    private readonly IFeatureService<Color> colorService;
    private readonly IFeatureService<Condition> conditionService;
    private readonly IFeatureService<EuroStandard> euroStandardService;
    private readonly IPopulatedPlaceService populatedPlaceService;
    private readonly IModelService modelService;

    public FeaturesController(
        IFeatureService<Transmission> transmissionService,
        IFeatureService<Engine> engineService,
        IFeatureService<BodyStyle> bodyStyleService,
        IFeatureService<Brand> brandService,
        IFeatureService<Region> regionService,
        IFeatureService<Color> colorService,
        IFeatureService<Condition> conditionService,
        IFeatureService<EuroStandard> euroStandardService,
        IPopulatedPlaceService populatedPlaceService,
        IModelService modelService)
    {
        this.transmissionService = transmissionService;
        this.engineService = engineService;
        this.bodyStyleService = bodyStyleService;
        this.brandService = brandService;
        this.regionService = regionService;
        this.colorService = colorService;
        this.conditionService = conditionService;
        this.euroStandardService = euroStandardService;
        this.populatedPlaceService = populatedPlaceService;
        this.modelService = modelService;
    }

    [HttpGet("transmissions")]
    public IEnumerable<FeatureResultModel> GetAllTransmissions() => transmissionService.GetAll();

    [HttpGet("engines")]
    public IEnumerable<FeatureResultModel> GetAllEngines() => engineService.GetAll();

    [HttpGet("body-styles")]
    public IEnumerable<FeatureResultModel> GetAllBodyStyles() => bodyStyleService.GetAll();

    [HttpGet("brands")]
    public IEnumerable<FeatureResultModel> GetAllBrands() => brandService.GetAll();

    [HttpGet("regions")]
    public IEnumerable<FeatureResultModel> GetAllRegions() => regionService.GetAll();

    [HttpGet("colors")]
    public IEnumerable<FeatureResultModel> GetAllColors() => colorService.GetAll();

    [HttpGet("conditions")]
    public IEnumerable<FeatureResultModel> GetAllConditions() => conditionService.GetAll();

    [HttpGet("euro-standards")]
    public IEnumerable<FeatureResultModel> GetAllEuroStandards() => euroStandardService.GetAll();

    [HttpGet("populated-places/{region?}")]
    public IEnumerable<FeatureResultModel> GetAllPopulatedPlacesByRegion(string? region)
        => populatedPlaceService.FindAllByRegion(region);

    [HttpGet("models/{brand?}")]
    public IEnumerable<FeatureResultModel> GetAllModelsByBrand(string? brand)
        => modelService.FindAllByBrand(brand);
}
