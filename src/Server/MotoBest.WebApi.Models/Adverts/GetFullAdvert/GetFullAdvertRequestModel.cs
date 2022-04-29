using MotoBest.Common.Units;

namespace MotoBest.WebApi.Models.Adverts.GetFullAdvert;

public class GetFullAdvertRequestModel : IUnitableModel
{
    public string Id { get; init; } = string.Empty;

    public CurrencyUnit CurrencyUnit { get; init; }

    public PowerUnit PowerUnit { get; init; }

    public MileageUnit MileageUnit { get; init; }
}
