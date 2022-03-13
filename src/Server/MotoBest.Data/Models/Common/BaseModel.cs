namespace MotoBest.Data.Models.Common;

public class BaseModel<TKey>
{
    public TKey Id { get; set; } = default!;
}
