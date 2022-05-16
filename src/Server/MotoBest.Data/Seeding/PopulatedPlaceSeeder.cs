using MotoBest.Common.Extensions;

namespace MotoBest.Data.Seeding;

public class PopulatedPlaceSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        string path = $"{GlobalConstants.SeedingResourcesPath}/populated-places-by-region.json";
        string json = await File.ReadAllTextAsync(path);

        var regionSeedingModels = json.ParseJsonTo<RegionSeedingModel[]>()!;

        foreach (var regionSeedingModel in regionSeedingModels)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Name == regionSeedingModel.Name);

            if (region == null)
            {
                region = new Region { Name = regionSeedingModel.Name };
                await dbContext.Regions.AddAsync(region);
            }

            await SeedPopulatedPlacesAsync(regionSeedingModel.PopulatedPlaces, dbContext, region.Id);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedPopulatedPlacesAsync(
        IEnumerable<PopulatedPlaceSeedingModel> populatedPlaceSeedingModels, AppDbContext dbContext, int regionId)
    {
        foreach (var populatedPlaceSeedingModel in populatedPlaceSeedingModels)
        {
            bool doesPopulatedPlaceExist = dbContext.PopulatedPlaces.Any(
                pp => pp.Name == populatedPlaceSeedingModel.Name && pp.RegionId == regionId);

            if (doesPopulatedPlaceExist)
            {
                continue;
            }

            await dbContext.AddAsync(new PopulatedPlace
            {
                Type = populatedPlaceSeedingModel.Type,
                Name = populatedPlaceSeedingModel.Name,
                RegionId = regionId,
            });
        }
    }
}
