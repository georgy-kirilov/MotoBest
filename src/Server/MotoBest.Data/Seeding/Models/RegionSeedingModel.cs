namespace MotoBest.Data.Seeding.Models;

public record class RegionSeedingModel(string Name, IEnumerable<PopulatedPlaceSeedingModel> PopulatedPlaces);
