using MotoBest.Common;

namespace MotoBest.Services;

public class StaticCurrencyCourseProvider : ICurrencyCourseProvider
{
    public decimal GetCourseToBgn(Currency currency)
        => currency switch
        {
            Currency.Bgn => 1m,
            Currency.Usd => 1.77m,
            Currency.Eur => 1.96m,
            _ => throw new NotSupportedException(
                $"{nameof(Currency)} enum value not supported.")
        };
}
