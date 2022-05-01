namespace MotoBest.WebApi.Models.Features;

public abstract class FeatureResultBaseModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
