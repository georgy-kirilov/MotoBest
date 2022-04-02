using MotoBest.Services;

using System;

namespace MotoBest.Tests.Mocks;

public class FakeDateTimeManager : IDateTimeManager
{
    public const string FakeTodayDateAsText = "2022-03-25";

    public DateTime Today()
    {
        return DateTime.Parse(FakeTodayDateAsText);
    }
}
