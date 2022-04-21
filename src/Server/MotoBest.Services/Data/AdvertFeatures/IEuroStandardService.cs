using MotoBest.Data.Models;

namespace MotoBest.Services.Data.AdvertFeatures;

public interface IEuroStandardService : IAdvertFeatureService<EuroStandard>
{
    /// <summary>
    /// Creates an approximation for the EuroStandard of a car
    /// </summary>
    /// <param name="manufacturedOn">The manufacturing date of the car</param>
    /// <returns>The closest value found or null if the manufacturing date is not supported</returns>
    Task<EuroStandard?> ApproximateAsync(DateTime? manufacturedOn);
}
