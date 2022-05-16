using MotoBest.Common.Extensions;

namespace MotoBest.Data.Seeding;

public class ModelSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        string path = $"{GlobalConstants.SeedingResourcesPath}/models-by-brand.json";
        string json = await File.ReadAllTextAsync(path);

        var brandSeedingModels = json.ParseJsonTo<BrandSeedingModel[]>()!;

        foreach (var brandSeedingModel in brandSeedingModels)
        {
            var brand = dbContext.Brands.FirstOrDefault(b => b.Name == brandSeedingModel.Name);

            if (brand == null)
            {
                brand = new Brand { Name = brandSeedingModel.Name };
                await dbContext.Brands.AddAsync(brand);
            }

            await SeedPopulatedPlacesAsync(brandSeedingModel.Models, dbContext, brand.Id);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedPopulatedPlacesAsync(
        IEnumerable<ModelSeedingModel> modelSeedingModels, AppDbContext dbContext, int brandId)
    {
        foreach (var modelSeedingModel in modelSeedingModels)
        {
            bool doesModelExist = dbContext.Models.Any(
                m => m.Name == modelSeedingModel.Name && m.BrandId == brandId);

            if (doesModelExist)
            {
                continue;
            }

            await dbContext.AddAsync(new Model
            {
                Name = modelSeedingModel.Name,
                BrandId = brandId,
            });
        }
    }
}
