using MotoBest.Common.Units;

namespace MotoBest.Services.Common.Units;

public interface IUnitsManager
{
    decimal GetBgnCourse(CurrencyUnit currencyUnit);

    double GetHpMultiplier(PowerUnit powerUnit);

    double GetKmMultiplier(MileageUnit mileageUnit);
}
