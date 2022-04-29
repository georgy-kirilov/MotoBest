namespace MotoBest.Data.Seeding;

public class AppSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var seeders = new List<ISeeder>
        {
            new ConstantAdvertFeatureSeeder<Transmission, TransmissionNames>(),
            new ConstantAdvertFeatureSeeder<Engine, EngineNames>(),
            new ConstantAdvertFeatureSeeder<BodyStyle, BodyStyleNames>(),
            new ConstantAdvertFeatureSeeder<Condition, ConditionNames>(),
            new ConstantAdvertFeatureSeeder<Brand, BrandNames>(),
            new ConstantAdvertFeatureSeeder<Region, RegionNames>(),
            new ConstantAdvertFeatureSeeder<Color, ColorNames>(),
            new RoleSeeder(),
            new PopulatedPlaceSeeder(),
            new EuroStandardSeeder(),
            new SiteSeeder(),
            new ModelSeeder(),
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
        }
    }
}
