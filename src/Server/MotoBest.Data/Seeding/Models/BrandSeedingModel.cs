namespace MotoBest.Data.Seeding.Models;

public record class BrandSeedingModel(string Name, IEnumerable<ModelSeedingModel> Models);
