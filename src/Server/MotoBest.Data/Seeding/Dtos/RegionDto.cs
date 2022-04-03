namespace MotoBest.Data.Seeding.Dtos;

public record class RegionDto(string Name, IEnumerable<PopulatedPlaceDto> PopulatedPlaces);