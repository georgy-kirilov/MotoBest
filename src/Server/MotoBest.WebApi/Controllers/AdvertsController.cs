using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoBest.Services.Common.Units;
using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;
using MotoBest.WebApi.Models.Adverts;

namespace MotoBest.WebApi.Controllers;

public class AdvertsController : ApiController
{
    private readonly IAdvertService advertService;
    private readonly IMapper mapper;
    private readonly IUnitsManager unitsManager;

    public AdvertsController(IAdvertService advertService, IMapper mapper, IUnitsManager unitsManager)
    {
        this.advertService = advertService;
        this.mapper = mapper;
        this.unitsManager = unitsManager;
    }

    [HttpGet("search")]
    public IEnumerable<SearchAdvertResultResponseModel> Search([FromQuery] SearchAdvertsRequestModel requestModel)
        => advertService
            .SearchAdverts(mapper.Map<SearchAdvertsServiceModel>(requestModel))
            .Select(a =>
            {
                var result = mapper.Map<SearchAdvertResultResponseModel>(a);

                result.CurrencyUnit = requestModel.CurrencyUnit.ToString();
                result.PowerUnit = requestModel.PowerUnit.ToString();
                result.MileageUnit = requestModel.MileageUnit.ToString();

                result.Price = (int?)(a.PriceInBgn / unitsManager.GetBgnCourse(requestModel.CurrencyUnit));
                result.Mileage = (int?)(a.MileageInKm / unitsManager.GetKmMultiplier(requestModel.MileageUnit));
                result.Power = (int?)(a.PowerInHp / unitsManager.GetHpMultiplier(requestModel.PowerUnit));

                return result;
            });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var advert = await advertService.GetFullAdvert(id);
        return advert == null ? NotFound() : Ok(advert);
    }
}
