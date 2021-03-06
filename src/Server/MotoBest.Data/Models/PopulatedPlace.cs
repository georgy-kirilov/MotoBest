namespace MotoBest.Data.Models;

public class PopulatedPlace : Feature
{
    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = default!;

    public PopulatedPlaceType Type { get; set; }
}
