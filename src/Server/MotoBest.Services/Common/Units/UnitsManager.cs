using MotoBest.Common.Exceptions;
using MotoBest.Common.Units;

namespace MotoBest.Services.Common.Units;

public class UnitsManager : IUnitsManager
{
    private readonly ICurrencyCourseProvider currencyCourseProvider;

    public UnitsManager(ICurrencyCourseProvider currencyCourseProvider)
    {
        this.currencyCourseProvider = currencyCourseProvider;
    }

    public decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => currencyCourseProvider.GetBgnCourse(currencyUnit);

    public double GetHpMultiplier(PowerUnit powerUnit)
        => powerUnit switch
        {
            PowerUnit.Hp => 1,
            PowerUnit.Kw => 1.34102209,
            PowerUnit.Ps => 0.9863,
            _ => throw new EnumValueNotSupportedException<PowerUnit>(powerUnit)
        };

    public double GetKmMultiplier(MileageUnit mileageUnit)
        => mileageUnit switch
        {
            MileageUnit.Km => 1,
            MileageUnit.Mi => 1.609344,
            _ => throw new EnumValueNotSupportedException<MileageUnit>(mileageUnit)
        };
}
