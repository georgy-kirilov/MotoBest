using System.ComponentModel.DataAnnotations;

namespace MotoBest.Data.Models.Common;

public class BaseModel<TKey>
{
    [Key]
    public TKey Id { get; set; } = default!;
}
