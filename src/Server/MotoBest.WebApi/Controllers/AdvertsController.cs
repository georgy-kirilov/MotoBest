using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;
using MotoBest.Services.Mapping;

using MotoBest.WebApi.Models.Adverts.GetFullAdvert;
using MotoBest.WebApi.Models.Adverts.SearchAdverts;

namespace MotoBest.WebApi.Controllers;

public class AdvertsController : ApiController
{
    private readonly IAdvertService advertService;
    private readonly IMapper mapper;
    private readonly AdvertMapper advertMapper;

    public AdvertsController(IAdvertService advertService, IMapper mapper, AdvertMapper advertMapper)
    {
        this.advertService = advertService;
        this.mapper = mapper;
        this.advertMapper = advertMapper;
    }

    [HttpGet("search")]
    public IEnumerable<SearchAdvertsResponseModel> SearchAdverts([FromQuery] SearchAdvertsRequestModel request)
        => advertService
            .SearchAdverts(mapper.Map<SearchAdvertsInputModel>(request))
            .Select(a => advertMapper.MapUnits<SearchAdvertsResponseModel>(a, request));

    [HttpGet]
    public async Task<IActionResult> GetFullAdvert([FromQuery] GetFullAdvertRequestModel request)
    {
        var result = await advertService.GetFullAdvert(request.Id);

        if (result == null)
        {
            return NotFound();
        }

        var response = advertMapper.MapUnits<GetFullAdvertResponseModel>(result, request);

        return Ok(response);
    }
}
