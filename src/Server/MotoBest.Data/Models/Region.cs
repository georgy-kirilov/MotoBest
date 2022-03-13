namespace MotoBest.Data.Models;

public class Region : Feature
{
    public Region()
    {
        Towns = new HashSet<Town>();
    }

    public virtual ICollection<Town> Towns { get; set; }
}
