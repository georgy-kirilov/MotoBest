using MotoBest.WebApi.Models.Adverts;
using MotoBest.WebApi.Models.Adverts.GetFullAdvert;

namespace MotoBest.Services.Data.Adverts.Models;

public class GetFullAdvertResultModel : GetFullAdvertBaseModel, INormalizedUnitableModel
{
    public decimal? PriceInBgn { get; init; }

    public int? PowerInHp { get; init; }

    public int? MileageInKm { get; init; }
}
