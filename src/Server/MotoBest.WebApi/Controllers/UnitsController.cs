using Microsoft.AspNetCore.Mvc;
using MotoBest.Common.Units;
using MotoBest.Services.Common.Units;
using MotoBest.WebApi.Models.Units;

namespace MotoBest.WebApi.Controllers;

public class UnitsController : ApiController
{
    private readonly IUnitManager unitManager;

    public UnitsController(IUnitManager unitManager)
    {
        this.unitManager = unitManager;
    }

    [HttpGet("currency/all")]
    public IEnumerable<GetAllUnitsResultModel> GetAllCurrencyUnits()
        => unitManager.GetAllUnits<CurrencyUnit>();

    [HttpGet("power/all")]
    public IEnumerable<GetAllUnitsResultModel> GetAllPowerUnits()
        => unitManager.GetAllUnits<PowerUnit>();

    [HttpGet("mileage/all")]
    public IEnumerable<GetAllUnitsResultModel> GetAllMileageUnits()
        => unitManager.GetAllUnits<MileageUnit>();
}
