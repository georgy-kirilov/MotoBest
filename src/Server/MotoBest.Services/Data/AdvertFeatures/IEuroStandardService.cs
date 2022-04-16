using MotoBest.Data.Models;

namespace MotoBest.Services.Data.AdvertFeatures;

public interface IEuroStandardService : IAdvertFeatureService<EuroStandard>
{
    Task<EuroStandard?> ApproximateAsync(DateTime? manufacturedOn);
}
