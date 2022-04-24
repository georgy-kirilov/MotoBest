using MotoBest.WebApi.Models.Adverts;

namespace MotoBest.Services.Data.Adverts.Models;

public class SearchAdvertResultServiceModel : SearchAdvertResultBaseModel
{
    public decimal? PriceInBgn { get; init; }

    public int? MileageInKm { get; init; }

    public int? PowerInHp { get; init; }
}
