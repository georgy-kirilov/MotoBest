using MotoBest.Common.Extensions;

namespace MotoBest.Data.Seeding;

public class ExtraSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        string path = $"{GlobalConstants.SeedingResourcesPath}/extras-by-type.json";
        string json = await File.ReadAllTextAsync(path);

        var extraSeedingModels = json.ParseJsonTo<ExtraSeedingModel[]>()!;

        foreach (var extraSeedingModel in extraSeedingModels)
        {
            var extraType = dbContext.ExtraTypes.FirstOrDefault(type => type.Name == extraSeedingModel.Type);

            if (extraType == null)
            {
                extraType = new ExtraType { Name = extraSeedingModel.Type };
                await dbContext.ExtraTypes.AddAsync(extraType);
                await dbContext.SaveChangesAsync();
            }

            foreach (string extraName in extraSeedingModel.Values)
            {
                if (dbContext.Extras.Any(ext => ext.Name == extraName))
                {
                    continue;
                }

                await dbContext.Extras.AddAsync(new Extra
                {
                    Name = extraName,
                    TypeId = extraType.Id,
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
