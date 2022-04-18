using AutoMapper;

using System.Globalization;

using MotoBest.Common;
using MotoBest.Data.Models;
using MotoBest.Services.Data.Adverts;

namespace MotoBest.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        var bulgarianCulture = new CultureInfo(GlobalConstants.BulgarianCultureInfo);

        CreateMap<Advert, AdvertSearchResult>()
            .ForMember(dest =>
                dest.Price,
                opt => opt.MapFrom((src, dest) => src.PriceBgn))
            .ForMember(
                dest => dest.Engine,
                opt => opt.MapFrom((src, dest) => src.Engine?.Name))
            .ForMember(
                dest => dest.Year,
                opt => opt.MapFrom((src, dest) => src.ManufacturedOn?.Year))
            .ForMember(
                dest => dest.Transmission,
                opt => opt.MapFrom((src, dest) => src.Transmission?.Name))
            .ForMember(
                dest => dest.MainImageUrl,
                opt => opt.MapFrom((src, dest) => src.Images.FirstOrDefault()?.Url ?? AdvertServiceConstants.DefaultAdvertImageUrl))
            .ForMember(
                dest => dest.Month,
                opt => opt.MapFrom((src, dest) => src.ManufacturedOn?.ToString("MMMM", bulgarianCulture)));
    }
}
