namespace MotoBest.Services.Data.Adverts.Models;

public class AdvertSearchFilterDto
{
    public string? Brand { get; init; }

    public string? Color { get; init; }

    public string? Condition { get; init; }

    public string? BodyStyle { get; init; }

    public string? Engine { get; init; }

    public string? Transmission { get; init; }

    public string? Region { get; init; }

    public int? MinHorsePowers { get; init; }

    public int? MaxHorsePowers { get; init; }

    public int? MinKilometrage { get; init; }

    public int? MaxKilometrage { get; init; }

    public int? MinYear { get; init; }

    public int? MaxYear { get; init; }
}
