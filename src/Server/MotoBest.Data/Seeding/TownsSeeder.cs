using MotoBest.Data.Seeding.Common;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MotoBest.Data.Seeding;

public class TownsSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var encoderSettings = new TextEncoderSettings();

        encoderSettings.AllowRanges(
            UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin);

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(encoderSettings),
            WriteIndented = true
        };

        string json = File.ReadAllText("../../Server/MotoBest.Data/Seeding/Resources/towns-by-regions.json");
        var regionDtos = JsonSerializer.Deserialize<RegionDto[]>(json, options)!;

        foreach (var regionDto in regionDtos)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Name == regionDto.Name);

            if (region == null)
            {
                continue;
            }

            foreach (var townDto in regionDto.Towns)
            {
                await dbContext.AddAsync(new Town
                {
                    IsVillage = townDto.IsVillage,
                    Name = townDto.Name,
                    RegionId = region.Id,
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }

    private record class TownDto(string Name, bool IsVillage);

    private record class RegionDto(string Name, IEnumerable<TownDto> Towns);
}
