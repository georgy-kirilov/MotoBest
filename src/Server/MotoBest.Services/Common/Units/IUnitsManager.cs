using MotoBest.Common.Units;

namespace MotoBest.Services.Common.Units;

public interface IUnitsManager
{
    decimal GetBgnCourse(CurrencyUnit currencyUnit);

    decimal? ToBgn(CurrencyUnit currencyUnit, decimal? value);

    double GetHpMultiplier(PowerUnit powerUnit);

    double? ToHp(PowerUnit powerUnit, double? value);

    double GetKmMultiplier(MileageUnit mileageUnit);

    double? ToKm(MileageUnit mileageUnit, double? value);
}
