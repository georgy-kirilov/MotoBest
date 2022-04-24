﻿namespace MotoBest.WebApi.Models.Adverts;

public abstract class SearchAdvertResultBaseModel
{
    public string Id { get; init; } = string.Empty;

    public string? Title { get; init; }

    public string? Month { get; init; }

    public int? Year { get; init; }

    public string? Transmission { get; init; }

    public string? Engine { get; init; }

    public DateTime? ModifiedOn { get; init; }

    public string? MainImageUrl { get; init; }
}