namespace MotoBest.Data.Models;

public class Extra : BaseModel<int>
{
    public Extra()
    {
        Adverts = new HashSet<Advert>();
    }

    public ExtraType Type { get; set; }

    public virtual ICollection<Advert> Adverts { get; set; }
}
