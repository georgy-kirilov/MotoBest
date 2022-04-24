namespace MotoBest.WebApi.Models.Adverts;

public class SearchAdvertResultResponseModel : SearchAdvertResultBaseModel
{
    public int? Power { get; set; }

    public string PowerUnit { get; set; } = string.Empty;

    public int? Mileage { get; set; }

    public string MileageUnit { get; set; } = string.Empty;

    public decimal? Price { get; set; }

    public string CurrencyUnit { get; set; } = string.Empty;
}
