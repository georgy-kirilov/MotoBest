namespace MotoBest.Data.Seeding.Models;

public record class RegionDto(string Name, IEnumerable<PopulatedPlaceDto> PopulatedPlaces);
