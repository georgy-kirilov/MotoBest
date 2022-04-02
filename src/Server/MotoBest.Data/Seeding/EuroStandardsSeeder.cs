using MotoBest.Data.Seeding.Common;
using MotoBest.Data.Seeding.Constants;

namespace MotoBest.Data.Seeding;

public class EuroStandardsSeeder : BaseAdvertFeaturesSeeder<EuroStandard>
{
    private static readonly EuroStandard[] euroStandards = new[]
    {
        new EuroStandard { Name = EuroStandardNames.EuroOne, FromDate = new DateTime(1992, 7, 1) },
        new EuroStandard { Name = EuroStandardNames.EuroTwo, FromDate = new DateTime(1996, 1, 1) },
        new EuroStandard { Name = EuroStandardNames.EuroThree, FromDate = new DateTime(2000, 1, 1) },
        new EuroStandard { Name = EuroStandardNames.EuroFour, FromDate = new DateTime(2005, 1, 1) },
        new EuroStandard { Name = EuroStandardNames.EuroFive, FromDate = new DateTime(2009, 9, 1) },
        new EuroStandard { Name = EuroStandardNames.EuroSix, FromDate = new DateTime(2014, 9, 1) },
    };

    public EuroStandardsSeeder() : base(euroStandards)
    { }
}
