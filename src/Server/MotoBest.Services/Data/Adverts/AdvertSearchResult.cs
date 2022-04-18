namespace MotoBest.Services.Data.Adverts;

public class AdvertSearchResult
{
    public string? Title { get; init; }

    public decimal? Price { get; init; }

    public string? Month { get; init; }

    public int? Year { get; init; }

    public int? Kilometrage { get; init; }

    public string? Transmission { get; init; }

    public string? Engine { get; init; }

    public int? HorsePowers { get; init; }

    public DateTime? ModifiedOn { get; init; }

    public string MainImageUrl { get; init; } = string.Empty;

}
