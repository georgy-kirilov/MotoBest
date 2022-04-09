using MotoBest.Data.Seeding.Common;
using MotoBest.Data.Seeding.Dtos;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MotoBest.Data.Seeding;

public class PopulatedPlacesSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin),
            WriteIndented = true
        };

        string path = "../../Server/MotoBest.Data/Seeding/Resources/populated-places-by-region.json";
        string json = await File.ReadAllTextAsync(path);

        var regionDtos = JsonSerializer.Deserialize<RegionDto[]>(json, options)!;

        foreach (var regionDto in regionDtos)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Name == regionDto.Name);

            if (region == null)
            {
                region = new Region { Name = regionDto.Name };
                await dbContext.Regions.AddAsync(region);
            }

            await SeedPopulatedPlacesAsync(regionDto.PopulatedPlaces, dbContext, region.Id);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedPopulatedPlacesAsync(
        IEnumerable<PopulatedPlaceDto> populatedPlaceDtos, AppDbContext dbContext, int regionId)
    {
        foreach (var populatedPlaceDto in populatedPlaceDtos)
        {
            if (dbContext.PopulatedPlaces.Any(pp => pp.Name == populatedPlaceDto.Name))
            {
                continue;
            }

            await dbContext.AddAsync(new PopulatedPlace
            {
                Type = populatedPlaceDto.Type,
                Name = populatedPlaceDto.Name,
                RegionId = regionId,
            });
        }
    }
}
