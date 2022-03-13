namespace MotoBest.Data.Seeding;

internal interface ISeeder
{
    Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider);
}
