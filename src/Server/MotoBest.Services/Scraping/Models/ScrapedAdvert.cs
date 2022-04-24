using MotoBest.Common.Units;
using MotoBest.Data.Models;

namespace MotoBest.Services.Scraping.Models;

public class ScrapedAdvert
{
    public string? RemoteId { get; set; }

    public string? Slug { get; set; }

    public string Site { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Mileage { get; set; }

    public int? Power { get; set; }

    public string? BodyStyle { get; set; }

    public string? Transmission { get; set; }

    public string? Engine { get; set; }

    public string? Condition { get; set; }

    public string? Color { get; set; }

    public DateTime? ManufacturedOn { get; set; }

    public CurrencyUnit? CurrencyUnit { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? PopulatedPlace { get; set; }

    public string? EuroStandard { get; set; }

    public string? Region { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
}
