using MotoBest.Common.Units;

namespace MotoBest.Services.Common.Units;

public interface ICurrencyCourseProvider
{
    decimal GetBgnCourse(CurrencyUnit currencyUnit);
}
