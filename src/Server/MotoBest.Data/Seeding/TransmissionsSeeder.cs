using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class TransmissionsSeeder : ConstantAdvertFeaturesSeeder<Transmission, TransmissionsSeeder>
{
    public const string Automatic = "Automatic";
    public const string Manual = "Manual";
    public const string SemiAutomatic = "Semi-Automatic";
    public const string Cvt = "CVT";
}
