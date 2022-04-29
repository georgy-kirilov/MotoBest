using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.Features;

public class EuroStandardService : FeatureService<EuroStandard>, IEuroStandardService
{
    public EuroStandardService(IRepository<EuroStandard> featureRepository)
        : base(featureRepository)
    {
    }

    public async Task<EuroStandard?> Approximate(DateTime? manufacturedOn)
        => await featureRepository.All()
            .Where(es => es.FromDate > manufacturedOn)
            .OrderByDescending(es => es.FromDate)
            .FirstOrDefaultAsync();
}
