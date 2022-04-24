using MotoBest.Common.Units;

namespace MotoBest.WebApi.Models.Adverts;

public class SearchAdvertsRequestModel : SearchAdvertsBaseModel
{
    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public CurrencyUnit CurrencyUnit { get; init; }

    public int? MinMileage { get; init; }

    public int? MaxMileage { get; init; }

    public MileageUnit MileageUnit { get; init; }

    public int? MinPower { get; init; }

    public int? MaxPower { get; init; }

    public PowerUnit PowerUnit { get; init; }
}
