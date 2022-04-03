namespace MotoBest.Data.Models;

public class Region : AdvertFeature
{
    public Region()
    {
        PopulatedPlaces = new HashSet<PopulatedPlace>();
    }

    public virtual ICollection<PopulatedPlace> PopulatedPlaces { get; set; }
}
