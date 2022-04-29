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

    public virtual decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => currencyCourseProvider.GetBgnCourse(currencyUnit);

    public decimal? ToBgn(CurrencyUnit currencyUnit, decimal? value)
        => GetBgnCourse(currencyUnit) * value;

    public virtual double GetHpMultiplier(PowerUnit powerUnit)
        => powerUnit switch
        {
            PowerUnit.Hp => 1,
            PowerUnit.Kw => 1.34102209,
            PowerUnit.Ps => 0.9863,
            _ => throw new EnumValueNotSupportedException<PowerUnit>(powerUnit)
        };

    public double? ToHp(PowerUnit powerUnit, double? value)
        => GetHpMultiplier(powerUnit) * value;

    public virtual double GetKmMultiplier(MileageUnit mileageUnit)
        => mileageUnit switch
        {
            MileageUnit.Km => 1,
            MileageUnit.Mi => 1.609344,
            _ => throw new EnumValueNotSupportedException<MileageUnit>(mileageUnit)
        };

    public double? ToKm(MileageUnit mileageUnit, double? value)
        => GetKmMultiplier(mileageUnit) * value;
}
