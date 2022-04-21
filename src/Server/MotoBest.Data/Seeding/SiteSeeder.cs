namespace MotoBest.Data.Seeding;

public class SiteSeeder : BaseAdvertFeatureSeeder<Site>
{
    private static readonly Site[] sites = new Site[]
    {
        new() { Name = SiteNames.AutoBg, FullAdvertPagePathFormat = "/obiava/{0}/{1}" }
    };

    public SiteSeeder() : base(sites)
    {
    }
}
