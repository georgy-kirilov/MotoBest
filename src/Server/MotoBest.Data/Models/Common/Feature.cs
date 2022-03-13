namespace MotoBest.Data.Models.Common;

public abstract class Feature : BaseModel<int>
{
    protected Feature()
    {
        Adverts = new HashSet<Advert>();
    }

    public string Name { get; set; } = "";

    public virtual ICollection<Advert> Adverts { get; set; }
}
