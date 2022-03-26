using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class EuroStandardsSeeder : AdvertFeaturesSeeder<EuroStandard>
{
    public const string EuroOne = "Euro 1";
    public const string EuroTwo = "Euro 2";
    public const string EuroThree = "Euro 3";
    public const string EuroFour = "Euro 4";
    public const string EuroFive = "Euro 5";
    public const string EuroSix = "Euro 6";

    private static readonly EuroStandard[] euroStandards = new[]
    {
        new EuroStandard { Name = EuroOne, FromDate = new DateTime(1992, 7, 1) },
        new EuroStandard { Name = EuroTwo, FromDate = new DateTime(1996, 1, 1) },
        new EuroStandard { Name = EuroThree, FromDate = new DateTime(2000, 1, 1) },
        new EuroStandard { Name = EuroFour, FromDate = new DateTime(2005, 1, 1) },
        new EuroStandard { Name = EuroFive, FromDate = new DateTime(2009, 9, 1) },
        new EuroStandard { Name = EuroSix, FromDate = new DateTime(2014, 9, 1) },
    };

    public EuroStandardsSeeder() : base(euroStandards)
    {
    }
}
