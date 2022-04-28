using MotoBest.WebApi.Models.Adverts.SearchAdverts;

namespace MotoBest.Services.Data.Adverts.Models;

public class SearchAdvertsInputModel : SearchAdvertsFilterBaseModel
{
    public int? MinPowerInHp { get; init; }

    public int? MaxPowerInHp { get; init; }

    public int? MinMileageInKm { get; init; }

    public int? MaxMileageInKm { get; init; }

    public decimal? MinPriceInBgn { get; init; }

    public decimal? MaxPriceInBgn { get; init; }
}
