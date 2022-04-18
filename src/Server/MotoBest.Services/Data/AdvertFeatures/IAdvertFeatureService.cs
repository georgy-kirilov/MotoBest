﻿using MotoBest.Data.Models.Common;

namespace MotoBest.Services.Data.AdvertFeatures;

public interface IAdvertFeatureService<TFeature>
    where TFeature : AdvertFeature, new()
{
    /// <summary>
    /// Returns the id of the model with the given name or null if such is not found
    /// </summary>
    int? FindIdByName(string? name);

    /// <summary>
    /// Returns the model with the given name or null if such is not found
    /// </summary>
    TFeature? FindByName(string? name);
}
