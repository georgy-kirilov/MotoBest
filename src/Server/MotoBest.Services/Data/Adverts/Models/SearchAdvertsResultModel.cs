using MotoBest.WebApi.Models.Adverts;
using MotoBest.WebApi.Models.Adverts.SearchAdverts;

namespace MotoBest.Services.Data.Adverts.Models;

public class SearchAdvertsResultModel : SearchAdvertsBaseModel, INormalizedUnitableModel
{
    public decimal? PriceInBgn { get; set; }

    public int? PowerInHp { get; set; }

    public int? MileageInKm { get; set; }
}
