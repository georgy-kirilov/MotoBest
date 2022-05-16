using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace MotoBest.Data.Seeding;

public class ModelSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin),
            WriteIndented = true
        };

        string path = "../../Server/MotoBest.Data/Seeding/Resources/models-by-brand.json";
        string json = await File.ReadAllTextAsync(path);

        var brandSeedingModels = JsonSerializer.Deserialize<BrandSeedingModel[]>(json, options)!;

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
