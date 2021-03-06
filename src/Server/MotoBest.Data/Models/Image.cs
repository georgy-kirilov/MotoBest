namespace MotoBest.Data.Models;

public class Image : BaseModel<int>
{
    public string? Url { get; set; }

    public string AdvertId { get; set; } = string.Empty;

    public virtual Advert Advert { get; set; } = default!;
}
