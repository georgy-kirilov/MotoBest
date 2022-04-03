namespace MotoBest.Data.Models;

public class PopulatedPlace : AdvertFeature
{
    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = default!;

    public PopulatedPlaceType Type { get; set; }
}
