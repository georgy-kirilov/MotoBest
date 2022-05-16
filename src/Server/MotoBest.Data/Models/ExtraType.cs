namespace MotoBest.Data.Models;

public class ExtraType : BaseModel<int>
{
    public ExtraType()
    {
        Extras = new HashSet<Extra>();
    }

    public string Name { get; set; } = default!;

    public virtual ICollection<Extra> Extras { get; set; }
}
