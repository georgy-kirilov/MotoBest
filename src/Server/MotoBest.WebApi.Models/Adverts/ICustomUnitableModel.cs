namespace MotoBest.WebApi.Models.Adverts;

public interface ICustomUnitableModel
{
    int? Price { get; set; }

    string CurrencyUnit { get; set; }

    int? Power { get; set; }

    string PowerUnit { get; set; }

    int? Mileage { get; set; }

    string MileageUnit { get; set; }
}
