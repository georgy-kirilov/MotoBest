namespace MotoBest.Data.Models;

public class Model : AdvertFeature
{
    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = default!;
}
