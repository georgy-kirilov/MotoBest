using MotoBest.Common;
using MotoBest.Services;

using System;

namespace MotoBest.Tests.Mocks;

public class FakeCurrencyCourseProvider : ICurrencyCourseProvider
{
    public decimal GetCourseToBgn(Currency currency)
        => currency switch
        {
            Currency.Bgn => 1,
            Currency.Usd => 2,
            Currency.Eur => 3,
            _ => throw new NotSupportedException($"{nameof(Currency)} enum value is not supported."),
        };
}
