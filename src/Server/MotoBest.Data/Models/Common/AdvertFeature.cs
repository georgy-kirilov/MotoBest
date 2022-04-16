namespace MotoBest.Data.Models.Common;

public abstract class AdvertFeature : BaseModel<int>
{
    protected AdvertFeature()
    {
        Adverts = new HashSet<Advert>();
    }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Advert> Adverts { get; set; }
}
