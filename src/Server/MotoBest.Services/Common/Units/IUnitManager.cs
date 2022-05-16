using MotoBest.Common.Units;
using MotoBest.WebApi.Models.Units;

namespace MotoBest.Services.Common.Units;

public interface IUnitManager
{
    decimal GetBgnCourse(CurrencyUnit currencyUnit);

    decimal? ToBgn(CurrencyUnit currencyUnit, decimal? value);

    double GetHpMultiplier(PowerUnit powerUnit);

    double? ToHp(PowerUnit powerUnit, double? value);

    double GetKmMultiplier(MileageUnit mileageUnit);

    double? ToKm(MileageUnit mileageUnit, double? value);

    IEnumerable<GetAllUnitsResultModel> GetAllUnits<TEnum>() where TEnum : struct, Enum;
}
