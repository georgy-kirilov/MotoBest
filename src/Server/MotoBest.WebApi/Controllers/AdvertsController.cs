using Microsoft.AspNetCore.Mvc;
using MotoBest.Services.Data.Adverts;

namespace MotoBest.WebApi.Controllers;

public class AdvertsController : ApiController
{
    private readonly IAdvertService advertService;

    public AdvertsController(IAdvertService advertService)
    {
        this.advertService = advertService;
    }

    [HttpGet]
    public IEnumerable<AdvertSearchResult> Search([FromQuery] AdvertSearchFilter filter)
    {
        return advertService.SearchAdverts(filter);
    }
}
