namespace MotoBest.Data.Models;

public class Town : AdvertFeature
{
    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = default!;
}
