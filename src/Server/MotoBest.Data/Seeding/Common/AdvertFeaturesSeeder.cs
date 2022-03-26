namespace MotoBest.Data.Seeding.Common;

public abstract class AdvertFeaturesSeeder<TFeature> : ISeeder
    where TFeature : AdvertFeature, new()
{
    private readonly IEnumerable<TFeature> models;

    protected AdvertFeaturesSeeder(IEnumerable<TFeature> models)
    {
        this.models = models;
    }

    public virtual async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var dbSet = dbContext.Set<TFeature>();

        foreach (var model in models)
        {
            if (dbSet.Any(m => m.Id == model.Id || m.Name == model.Name))
            {
                continue;
            }

            await dbContext.AddAsync(model);
        }

        await dbContext.SaveChangesAsync();
    }
}
