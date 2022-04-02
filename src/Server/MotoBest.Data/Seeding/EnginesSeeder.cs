using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class EnginesSeeder : ConstantAdvertFeaturesSeeder<Engine, EnginesSeeder>
{
    public const string Diesel = "Diesel";
    public const string Gasoline = "Gasoline";
    public const string Hybrid = "Hybrid";
    public const string Electric = "Electric";
}
