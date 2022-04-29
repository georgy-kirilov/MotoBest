using Microsoft.AspNetCore.Mvc;

using MotoBest.Data.Models;
using MotoBest.Services.Data.Features;

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

    public FeaturesController(
        IFeatureService<Transmission> transmissionService,
        IFeatureService<Engine> engineService,
        IFeatureService<BodyStyle> bodyStyleService,
        IFeatureService<Brand> brandService,
        IFeatureService<Region> regionService,
        IFeatureService<Color> colorService,
        IFeatureService<Condition> conditionService)
    {
        this.transmissionService = transmissionService;
        this.engineService = engineService;
        this.bodyStyleService = bodyStyleService;
        this.brandService = brandService;
        this.regionService = regionService;
        this.colorService = colorService;
        this.conditionService = conditionService;
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
}
