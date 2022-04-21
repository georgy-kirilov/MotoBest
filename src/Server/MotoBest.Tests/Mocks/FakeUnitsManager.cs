using MotoBest.Common.Units;
using MotoBest.Services.Common.Units;

namespace MotoBest.Tests.Mocks;

public class FakeUnitsManager : IUnitsManager
{
    public decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => GetFakeMultiplier(currencyUnit);

    public double GetHpMultiplier(PowerUnit powerUnit)
        => GetFakeMultiplier(powerUnit);

    public double GetKmMultiplier(MileageUnit mileageUnit)
        => GetFakeMultiplier(mileageUnit);

    private static int GetFakeMultiplier<TEnum>(TEnum enumValue) where TEnum : Enum
    {
        int enumValueAsNumber = (int)(object)enumValue;
        return enumValueAsNumber + 1;
    }
}
