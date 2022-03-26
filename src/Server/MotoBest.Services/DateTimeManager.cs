namespace MotoBest.Services;

public class DateTimeManager : IDateTimeManager
{
    public DateTime Today()
    {
        return DateTime.Today;
    }
}
