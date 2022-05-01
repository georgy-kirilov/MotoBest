using AutoMapper;

using MotoBest.Data.Models;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Mapping;

public class FeatureProfile : Profile
{
    public FeatureProfile()
    {
        CreateMap<PopulatedPlace, GetAllPopulatedPlacesByRegionResultModel>();
        CreateMap<Model, GetAllModelsByBrandResultModel>();
    }
}
