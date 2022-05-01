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
    public IEnumerable<string> GetAllTransmissions() => transmissionService.GetAllNames();

    [HttpGet("engines")]
    public IEnumerable<string> GetAllEngines() => engineService.GetAllNames();

    [HttpGet("body-styles")]
    public IEnumerable<string> GetAllBodyStyles() => bodyStyleService.GetAllNames();

    [HttpGet("brands")]
    public IEnumerable<string> GetAllBrands() => brandService.GetAllNames();

    [HttpGet("regions")]
    public IEnumerable<string> GetAllRegions() => regionService.GetAllNames();

    [HttpGet("colors")]
    public IEnumerable<string> GetAllColors() => colorService.GetAllNames();

    [HttpGet("conditions")]
    public IEnumerable<string> GetAllConditions() => conditionService.GetAllNames();

    [HttpGet("euro-standards")]
    public IEnumerable<string> GetAllEuroStandards() => euroStandardService.GetAllNames();

    [HttpGet("populated-places/{region?}")]
    public IEnumerable<GetAllPopulatedPlacesByRegionResultModel> GetAllPopulatedPlacesByRegion(string? region)
        => populatedPlaceService.FindAllByRegion(region);

    [HttpGet("models/{brand?}")]
    public IEnumerable<GetAllModelsByBrandResultModel> GetAllModelsByBrand(string? brand)
        => modelService.FindAllByBrand(brand);
}
