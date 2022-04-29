using MotoBest.Common.Extensions;

namespace MotoBest.Data.Seeding.Common;

public class ConstantAdvertFeatureSeeder<TFeature, TConstantsSource> : BaseAdvertFeatureSeeder<TFeature>
    where TFeature : Feature, new()
{
    public ConstantAdvertFeatureSeeder()
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
