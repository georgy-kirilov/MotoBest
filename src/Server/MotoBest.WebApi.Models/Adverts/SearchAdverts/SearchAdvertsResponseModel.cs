namespace MotoBest.WebApi.Models.Adverts.SearchAdverts;

public class SearchAdvertsResponseModel : SearchAdvertBaseModel, ICustomUnitableModel
{
    public int? Power { get; set; }

    public string PowerUnit { get; set; } = string.Empty;

    public int? Mileage { get; set; }

    public string MileageUnit { get; set; } = string.Empty;

    public int? Price { get; set; }

    public string CurrencyUnit { get; set; } = string.Empty;
}
