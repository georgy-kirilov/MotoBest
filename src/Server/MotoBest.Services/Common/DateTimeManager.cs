namespace MotoBest.Services.Common;

public class DateTimeManager : IDateTimeManager
{
    public DateTime Today()
    {
        return DateTime.Today;
    }
}
