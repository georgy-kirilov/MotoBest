using MotoBest.Common;

namespace MotoBest.Services.Common;

public interface ICurrencyCourseProvider
{
    decimal GetCourseToBgn(Currency currency);
}
