using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class EnginesSeeder : AdvertFeaturesSeeder<Engine>
{
    public const string Diesel = "Diesel";
    public const string Gasoline = "Gasoline";
    public const string Hybrid = "Hybrid";
    public const string Electric = "Electric";

    private static readonly Engine[] engines = new[]
    {
        new Engine { Name = Diesel },
        new Engine { Name = Gasoline },
        new Engine { Name = Hybrid },
        new Engine { Name = Electric },
    };

    public EnginesSeeder() : base(engines)
    {
    }
}
