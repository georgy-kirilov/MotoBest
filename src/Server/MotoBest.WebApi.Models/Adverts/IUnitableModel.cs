using MotoBest.Common.Units;

namespace MotoBest.WebApi.Models.Adverts;

public interface IUnitableModel
{
    CurrencyUnit CurrencyUnit { get; init; }

    PowerUnit PowerUnit { get; init; }

    MileageUnit MileageUnit { get; init; }
}
