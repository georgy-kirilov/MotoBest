using Microsoft.AspNetCore.Mvc;

using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;

namespace MotoBest.WebApi.Controllers;

public class AdvertsController : ApiController
{
    private readonly IAdvertService advertService;

    public AdvertsController(IAdvertService advertService)
    {
        this.advertService = advertService;
    }

    [HttpGet]
    public IEnumerable<AdvertSearchResultDto> Search([FromQuery] AdvertSearchFilterDto filter)
    {
        return advertService.SearchAdverts(filter);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var advert = await advertService.GetFullAdvertAsync(id);
        return advert == null ? NotFound() : Ok(advert);
    }
}
