using MotoBest.Common.Exceptions;
using MotoBest.Common.Units;

namespace MotoBest.Services.Common.Units;

public class StaticCurrencyCourseProvider : ICurrencyCourseProvider
{
    public decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => currencyUnit switch
        {
            CurrencyUnit.Bgn => 1m,
            CurrencyUnit.Usd => 1.77m,
            CurrencyUnit.Eur => 1.96m,
            _ => throw new EnumValueNotSupportedException<CurrencyUnit>(currencyUnit)
        };
}
