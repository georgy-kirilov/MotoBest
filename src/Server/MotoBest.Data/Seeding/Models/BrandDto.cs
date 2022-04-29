namespace MotoBest.Data.Seeding.Models;

public record class BrandDto(string Name, IEnumerable<ModelDto> Models);
