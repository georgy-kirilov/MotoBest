namespace MotoBest.Data.Models;

public class Extra : Feature
{
    public int TypeId { get; set; }

    public virtual ExtraType Type { get; set; } = default!;
}
