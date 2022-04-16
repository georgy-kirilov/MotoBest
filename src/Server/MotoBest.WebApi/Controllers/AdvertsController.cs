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
    public async Task<IEnumerable<SearchAdvertResult>> Search([FromQuery] SearchAdvertsFilter filter)
    {
        return await advertService.SearchAdvertsAsync(filter);
    }
}
