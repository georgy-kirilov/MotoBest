using MotoBest.Data.Models;
using MotoBest.Data.Models.Common;

namespace MotoBest.Services.Normalization;

public class NormalizedAdvert
{
    public string? RemoteId { get; init; }

    public string? RemoteSlug { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }

    public string? BodyStyle { get; init; }

    public string? Transmission { get; init; }

    public string? Engine { get; init; }

    public string? Condition { get; init; }

    public int? MileageInKm { get; init; }

    public int? PowerInHp { get; init; }

    public string? Color { get; init; }

    public DateTime? ManufacturedOn { get; init; }

    public decimal? PriceInBgn { get; init; }

    public string? Brand { get; init; }

    public string? Model { get; init; }

    public string? PopulatedPlace { get; init; }

    public string? EuroStandard { get; init; }

    public string? Region { get; init; }

    public string Site { get; init; } = string.Empty;

    public DateTime? ModifiedOn { get; init; }

    public PopulatedPlaceType? PopulatedPlaceType { get; init; }

    public IEnumerable<string> ImageUrls { get; init; } = new List<string>();
}
