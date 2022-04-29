using AutoMapper;

using MotoBest.Services.Common.Units;
using MotoBest.WebApi.Models.Adverts;

namespace MotoBest.Services.Mapping;

public class AdvertMapper
{
    private readonly IMapper mapper;
    private readonly IUnitsManager unitsManager;

    public AdvertMapper(IMapper mapper, IUnitsManager unitsManager)
    {
        this.mapper = mapper;
        this.unitsManager = unitsManager;
    }

    public TResult MapUnits<TResult>(
        INormalizedUnitableModel modelToMapFrom,
        IUnitableModel unitable)
        where TResult : ICustomUnitableModel, new()
    {
        var result = mapper.Map<TResult>(modelToMapFrom);

        result.PowerUnit = unitable.PowerUnit.ToString().ToLower();
        result.CurrencyUnit = unitable.CurrencyUnit.ToString().ToLower();
        result.MileageUnit = unitable.MileageUnit.ToString().ToLower();

        result.Power = (int?)(modelToMapFrom.PowerInHp / unitsManager.GetHpMultiplier(unitable.PowerUnit));
        result.Price = (int?)(modelToMapFrom.PriceInBgn / unitsManager.GetBgnCourse(unitable.CurrencyUnit));
        result.Mileage = (int?)(modelToMapFrom.MileageInKm / unitsManager.GetKmMultiplier(unitable.MileageUnit));

        return result;
    }
}
