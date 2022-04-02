using MotoBest.Common;

namespace MotoBest.Services;

public interface ICurrencyCourseProvider
{
    decimal GetCourseToBgn(Currency currency);
}
