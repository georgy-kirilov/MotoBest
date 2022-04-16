using MotoBest.Common;
using MotoBest.Data.Models;

namespace MotoBest.Services.Scraping.Models;

public class ScrapedAdvert
{
    public string? RemoteId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? BodyStyle { get; set; }

    public string? Transmission { get; set; }

    public string? Engine { get; set; }

    public string? Condition { get; set; }

    public int? Kilometrage { get; set; }

    public int? HorsePowers { get; set; }

    public string? Color { get; set; }

    public DateTime? ManufacturedOn { get; set; }

    public decimal? Price { get; set; }

    public Currency? Currency { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? PopulatedPlace { get; set; }

    public string? EuroStandard { get; set; }

    public string? Region { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string Site { get; set; } = "";

    public PopulatedPlaceType? PopulatedPlaceType { get; set; }

    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
}
