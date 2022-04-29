using MotoBest.WebApi.Models.Adverts.SearchAdverts;

namespace MotoBest.Services.Data.Adverts.Models;

public class SearchAdvertsInputModel : SearchAdvertsBaseFilter
{
    public decimal? MinPriceInBgn { get; init; }

    public decimal? MaxPriceInBgn { get; init; }

    public int? MinPowerInHp { get; init; }

    public int? MaxPowerInHp { get; init; }

    public int? MinMileageInKm { get; init; }

    public int? MaxMileageInKm { get; init; }
}
