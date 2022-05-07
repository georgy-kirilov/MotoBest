namespace MotoBest.WebApi.Models.Adverts.SearchAdverts;

public abstract class SearchAdvertsBaseFilter
{
    public int? BrandId { get; init; }

    public int? ModelId { get; init; }

    public int? EngineId { get; init; }

    public int? BodyStyleId { get; init; }

    public int? TransmissionId { get; init; }

    public int? ColorId { get; init; }

    public int? EuroStandardId { get; init; }

    public int? ConditionId { get; init; }

    public int? RegionId { get; init; }

    public int? PopulatedPlaceId { get; init; }

    public int? MinYear { get; init; }

    public int? MaxYear { get; init; }
}
