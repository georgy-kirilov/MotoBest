namespace MotoBest.WebApi.Models;

public abstract class SearchAdvertsBaseModel
{
    public string? Brand { get; init; }

    public string? Model { get; init; }

    public string? Engine { get; init; }

    public string? BodyStyle { get; init; }

    public string? Transmission { get; init; }

    public string? Color { get; init; }

    public string? Condition { get; init; }

    public string? Region { get; init; }

    public string? PopulatedPlace { get; init; }

    public int? MinYear { get; init; }

    public int? MaxYear { get; init; }
}
