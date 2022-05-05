using AutoMapper;

using MotoBest.Data.Models.Common;
using MotoBest.WebApi.Models.Features;

namespace MotoBest.Services.Mapping;

public class FeatureProfile : Profile
{
    public FeatureProfile()
    {
        CreateMap<Feature, FeatureResultModel>();
    }
}
