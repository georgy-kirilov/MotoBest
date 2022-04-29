using MotoBest.Common.Units;
using MotoBest.Services.Common.Units;

namespace MotoBest.Tests.Mocks;

public class FakeUnitsManager : UnitsManager
{
    public FakeUnitsManager() : base(default!)
    {
    }

    public override decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => GetFakeMultiplier(currencyUnit);

    public override double GetHpMultiplier(PowerUnit powerUnit)
        => GetFakeMultiplier(powerUnit);

    public override double GetKmMultiplier(MileageUnit mileageUnit)
        => GetFakeMultiplier(mileageUnit);

    private static int GetFakeMultiplier<TEnum>(TEnum enumValue) where TEnum : Enum
    {
        int enumValueAsNumber = (int)(object)enumValue;
        return enumValueAsNumber + 1;
    }
}
