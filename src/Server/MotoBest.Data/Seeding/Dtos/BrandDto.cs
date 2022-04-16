namespace MotoBest.Data.Seeding.Dtos;

public record class BrandDto(string Name, IEnumerable<ModelDto> Models);
