namespace MotoBest.Data.Seeding.Common;

public class ConstantAdvertFeaturesSeeder<TFeature, TConstantsSource> : BaseAdvertFeaturesSeeder<TFeature>
    where TFeature : AdvertFeature, new()
{
    public ConstantAdvertFeaturesSeeder()
        : base(CreateModelsFromNameConstants())
    { }

    private static IEnumerable<TFeature> CreateModelsFromNameConstants()
    {
        var models = new List<TFeature>();

        foreach (string modelName in typeof(TConstantsSource).GetAllPublicConstantValues<string>())
        {
            models.Add(new TFeature { Name = modelName });
        }

        return models;
    }
}
