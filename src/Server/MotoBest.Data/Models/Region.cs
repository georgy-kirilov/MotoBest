namespace MotoBest.Data.Models;

public class Region : AdvertFeature
{
    public Region()
    {
        Towns = new HashSet<Town>();
    }

    public virtual ICollection<Town> Towns { get; set; }
}
