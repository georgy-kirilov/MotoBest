using AutoMapper;

using MotoBest.Services.Common.Units;
using MotoBest.WebApi.Models.Adverts;

namespace MotoBest.Services.Mapping;

public class AdvertMapper
{
    private readonly IMapper mapper;
    private readonly IUnitManager unitManager;

    public AdvertMapper(IMapper mapper, IUnitManager unitManager)
    {
        this.mapper = mapper;
        this.unitManager = unitManager;
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

        result.Power = (int?)(modelToMapFrom.PowerInHp / unitManager.GetHpMultiplier(unitable.PowerUnit));
        result.Price = (int?)(modelToMapFrom.PriceInBgn / unitManager.GetBgnCourse(unitable.CurrencyUnit));
        result.Mileage = (int?)(modelToMapFrom.MileageInKm / unitManager.GetKmMultiplier(unitable.MileageUnit));

        return result;
    }
}
