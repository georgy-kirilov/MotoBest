using MotoBest.Data.Seeding.Common;
using MotoBest.Data.Seeding.Constants;

namespace MotoBest.Data.Seeding;

public class SitesSeeder : BaseAdvertFeaturesSeeder<Site>
{
    private static readonly Site[] sites = new[]
    {
        new Site { Name = SiteNames.AutoBg, FullAdvertPagePathFormat = "/obiava/{0}/{1}" }
    };

    public SitesSeeder() : base(sites)
    {
    }
}
