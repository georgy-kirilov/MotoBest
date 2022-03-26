using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class TransmissionsSeeder : AdvertFeaturesSeeder<Transmission>
{
    public const string Automatic = "Automatic";
    public const string Manual = "Manual";
    public const string SemiAutomatic = "Semi-Automatic";
    public const string Cvt = "CVT";

    private static readonly Transmission[] transmissions = new[]
    {
        new Transmission { Name = Automatic },
        new Transmission { Name = Manual },
        new Transmission { Name = SemiAutomatic },
        new Transmission { Name = Cvt },
    };

    public TransmissionsSeeder() : base(transmissions)
    {
    }
}
