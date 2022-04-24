using MotoBest.Common.Units;

namespace MotoBest.WebApi.Models.Adverts;

public class GetFullAdvertRequestModel
{
    public CurrencyUnit CurrencyUnit { get; init; }

    public MileageUnit MileageUnit { get; init; }

    public PowerUnit PowerUnit { get; init; }
}
