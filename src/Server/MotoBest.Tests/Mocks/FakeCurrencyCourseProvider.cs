﻿using MotoBest.Common.Exceptions;
using MotoBest.Common.Units;
using MotoBest.Services.Common.Units;

namespace MotoBest.Tests.Mocks;

public class FakeCurrencyCourseProvider : ICurrencyCourseProvider
{
    public decimal GetBgnCourse(CurrencyUnit currencyUnit)
        => currencyUnit switch
        {
            CurrencyUnit.Bgn => 1,
            CurrencyUnit.Usd => 2,
            CurrencyUnit.Eur => 3,
            _ => throw new EnumValueNotSupportedException<CurrencyUnit>(currencyUnit)
        };
}
