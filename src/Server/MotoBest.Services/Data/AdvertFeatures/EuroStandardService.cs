﻿using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models;
using MotoBest.Data.Repositories;

namespace MotoBest.Services.Data.AdvertFeatures;

public class EuroStandardService : AdvertFeatureService<EuroStandard>, IEuroStandardService
{
    public EuroStandardService(IRepository<EuroStandard> featureRepository)
        : base(featureRepository)
    {
    }

    public async Task<EuroStandard?> ApproximateAsync(DateTime? manufacturedOn)
        => await featureRepository.All()
            .Where(es => es.FromDate > manufacturedOn)
            .OrderByDescending(es => es.FromDate)
            .FirstOrDefaultAsync();
}