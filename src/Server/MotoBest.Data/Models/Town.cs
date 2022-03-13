namespace MotoBest.Data.Models;

public class Town : Feature
{
    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = default!;
}
