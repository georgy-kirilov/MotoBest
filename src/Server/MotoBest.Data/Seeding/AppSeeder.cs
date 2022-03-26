using MotoBest.Data.Seeding.Common;

namespace MotoBest.Data.Seeding;

public class AppSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var seeders = new List<ISeeder>
        {
            new RolesSeeder(),
            new TransmissionsSeeder(),
            new EnginesSeeder(),
            new EuroStandardsSeeder(),
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
        }
    }
}
