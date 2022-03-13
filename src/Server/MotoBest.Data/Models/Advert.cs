using Microsoft.EntityFrameworkCore;

namespace MotoBest.Data.Models;

public class Advert : BaseModel<string>
{
    public Advert()
    {
        Id = Guid.NewGuid().ToString();
        Images = new HashSet<Image>();
    }

    /// <summary>
    /// Returns null if the advert belongs to MotoBest
    /// </summary>
    public string? RemoteId { get; set; } = "";

    /// <summary>
    /// Returns null if the advert belongs to MotoBest
    /// </summary>
    public int? SiteId { get; set; }

    /// <summary>
    /// Returns null if the advert belongs to MotoBest
    /// </summary>
    public virtual Site? Site { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    [Precision(14, 2)]
    public decimal? PriceBgn { get; set; }

    public DateTime? ManufacturedOn { get; set; }

    public int? Kilometrage { get; set; }

    public int? HorsePowers { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public int? ModelId { get; set; }

    public virtual Model? Model { get; set; }

    public int? BodyStyleId { get; set; }

    public virtual BodyStyle? BodyStyle { get; set; }

    public int? EngineId { get; set; }

    public virtual Engine? Engine { get; set; }

    public int? TransmissionId { get; set; }

    public virtual Transmission? Transmission { get; set; }

    public int? ConditionId { get; set; }

    public virtual Condition? Condition { get; set; }

    public int? ColorId { get; set; }

    public virtual Color? Color { get; set; }

    public int? RegionId { get; set; }

    public virtual Region? Region { get; set; }

    public int? TownId { get; set; }

    public virtual Town? Town { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}
