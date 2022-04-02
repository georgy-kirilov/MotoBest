namespace MotoBest.Data.Seeding.Common;

public abstract class ConstantAdvertFeaturesSeeder<TFeature, TConstantsSource> : BaseAdvertFeaturesSeeder<TFeature>
    where TFeature : AdvertFeature, new()
{
    protected static IEnumerable<TFeature> CreateModelsFromSeederConstants()
    {
        var models = new List<TFeature>();

        foreach (string modelName in typeof(TConstantsSource).GetAllPublicConstantValues<string>())
        {
            models.Add(new TFeature { Name = modelName });
        }

        return models;
    }

    protected ConstantAdvertFeaturesSeeder() : base(CreateModelsFromSeederConstants())
    {
    }
}
