namespace MotoBest.WebApi.Models.Adverts.SearchAdverts;

public abstract class SearchAdvertsBaseFilter
{
    public string? Brand { get; init; }

    public int? ModelId { get; init; }

    public string? Engine { get; init; }

    public string? BodyStyle { get; init; }

    public string? Transmission { get; init; }

    public string? Color { get; init; }

    public string? Condition { get; init; }

    public string? Region { get; init; }

    public int? PopulatedPlaceId { get; init; }

    public int? MinYear { get; init; }

    public int? MaxYear { get; init; }
}
