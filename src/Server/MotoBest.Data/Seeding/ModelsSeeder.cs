using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

using MotoBest.Data.Seeding.Common;
using MotoBest.Data.Seeding.Dtos;

namespace MotoBest.Data.Seeding;

public class ModelsSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin),
            WriteIndented = true
        };

        string path = "../../Server/MotoBest.Data/Seeding/Resources/models-by-brands.json";
        string json = await File.ReadAllTextAsync(path);

        var brandDtos = JsonSerializer.Deserialize<BrandDto[]>(json, options)!;

        foreach (var brandDto in brandDtos)
        {
            var brand = dbContext.Brands.FirstOrDefault(b => b.Name == brandDto.Name);

            if (brand == null)
            {
                brand = new Brand { Name = brandDto.Name };
                await dbContext.Brands.AddAsync(brand);
            }

            await SeedPopulatedPlacesAsync(brandDto.Models, dbContext, brand.Id);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedPopulatedPlacesAsync(
        IEnumerable<ModelDto> modelDtos, AppDbContext dbContext, int brandId)
    {
        foreach (var modelDto in modelDtos)
        {
            bool doesModelExist = dbContext.Models.Any(
                m => m.Name == modelDto.Name && m.BrandId == brandId);

            if (doesModelExist)
            {
                continue;
            }

            await dbContext.AddAsync(new Model
            {
                Name = modelDto.Name,
                BrandId = brandId,
            });
        }
    }
}
