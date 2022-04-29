using static MotoBest.Data.Seeding.Constants.EuroStandardNames;

namespace MotoBest.Data.Seeding;

public class EuroStandardSeeder : BaseAdvertFeatureSeeder<EuroStandard>
{
    private static readonly EuroStandard[] euroStandards = new EuroStandard[]
    {
        new() { Name = EuroOne, FromDate = new(1992, 7, 1) },
        new() { Name = EuroTwo, FromDate = new(1996, 1, 1) },
        new() { Name = EuroThree, FromDate = new(2000, 1, 1) },
        new() { Name = EuroFour, FromDate = new(2005, 1, 1) },
        new() { Name = EuroFive, FromDate = new(2009, 9, 1) },
        new() { Name = EuroSix, FromDate = new(2014, 9, 1) },
    };

    public EuroStandardSeeder() : base(euroStandards)
    {
    }
}
