using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class ConditionsSeeder : ConstantAdvertFeaturesSeeder<Condition, ConditionsSeeder>
{
    public const string New = "New";
    public const string Used = "Used";
    public const string DamagedOrHit = "Damaged/Hit";
    public const string ForParts = "For Parts";
}
