using MotoBest.Data.Seeding.Common;
using MotoBest.Data.Seeding.Constants;

namespace MotoBest.Data.Seeding;

public class AppSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var seeders = new List<ISeeder>
        {
            new ConstantAdvertFeaturesSeeder<Transmission, TransmissionNames>(),
            new ConstantAdvertFeaturesSeeder<Engine, EngineNames>(),
            new ConstantAdvertFeaturesSeeder<BodyStyle, BodyStyleNames>(),
            new ConstantAdvertFeaturesSeeder<Condition, ConditionNames>(),
            new ConstantAdvertFeaturesSeeder<Brand, BrandNames>(),
            new ConstantAdvertFeaturesSeeder<Region, RegionNames>(),
            new ConstantAdvertFeaturesSeeder<Color, ColorNames>(),
            new RolesSeeder(),
            new PopulatedPlacesSeeder(),
            new EuroStandardsSeeder(),
            new SitesSeeder(),
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
        }
    }
}
