namespace MotoBest.Data.Seeding.Common;

public interface ISeeder
{
    Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider);
}
