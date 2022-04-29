namespace MotoBest.WebApi.Models.Adverts;

public interface INormalizedUnitableModel
{
    public decimal? PriceInBgn { get; }

    public int? PowerInHp { get; }

    public int? MileageInKm { get; }
}
