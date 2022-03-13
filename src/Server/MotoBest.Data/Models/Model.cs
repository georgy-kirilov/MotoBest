namespace MotoBest.Data.Models;

public class Model : Feature
{
    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = default!;
}
