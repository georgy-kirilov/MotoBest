namespace MotoBest.WebApi.Models.Adverts.GetFullAdvert;

public abstract class GetFullAdvertBaseModel
{
    public string? OriginalAdvertUrl { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }

    public string? Brand { get; init; }

    public string? Model { get; init; }

    public string? Engine { get; init; }

    public string? Color { get; init; }

    public string? Condition { get; init; }

    public string? BodyStyle { get; init; }

    public string? Transmission { get; init; }

    public string? Region { get; init; }

    public string? PopulatedPlace { get; init; }

    public int? Year { get; init; }

    public string? Month { get; init; }

    public IEnumerable<string> ImageUrls { get; init; } = new List<string>();
}
