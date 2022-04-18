using AutoMapper;

using System.Globalization;

using MotoBest.Common;
using MotoBest.Data.Models;
using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;

namespace MotoBest.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Advert, AdvertSearchResultDto>()
            .ForMember(
                dest => dest.Price,
                opt => opt.MapFrom(MapPrice))
            .ForMember(
                dest => dest.Engine,
                opt => opt.MapFrom((src, dest) => src.Engine?.Name))
            .ForMember(
                dest => dest.Year,
                opt => opt.MapFrom(MapYear))
            .ForMember(
                dest => dest.Transmission,
                opt => opt.MapFrom(MapTransmission))
            .ForMember(
                dest => dest.MainImageUrl,
                opt => opt.MapFrom((src, dest) => src.Images.FirstOrDefault()?.Url ?? AdvertServiceConstants.DefaultAdvertImageUrl))
            .ForMember(
                dest => dest.Month,
                opt => opt.MapFrom(MapMonthName));

        CreateMap<Advert, FullAdvertDto>()
            .ForMember(
                dest => dest.Price,
                opt => opt.MapFrom(MapPrice))
            .ForMember(
                dest => dest.Year,
                opt => opt.MapFrom(MapYear))
            .ForMember(
                dest => dest.Month,
                opt => opt.MapFrom(MapMonthName))
            .ForMember(
                dest => dest.Transmission,
                opt => opt.MapFrom(MapTransmission))
            .ForMember(
                dest => dest.BodyStyle,
                opt => opt.MapFrom(MapBodyStyle))
            .ForMember(
                dest => dest.Engine,
                opt => opt.MapFrom(MapEngine))
            .ForMember(
                dest => dest.Brand,
                opt => opt.MapFrom((src, dest) => src.Brand?.Name))
            .ForMember(
                dest => dest.Model,
                opt => opt.MapFrom((src, dest) => src.Model?.Name))
            .ForMember(
                dest => dest.Color,
                opt => opt.MapFrom((src, dest) => src.Color?.Name))
            .ForMember(
                dest => dest.Condition,
                opt => opt.MapFrom((src, dest) => src.Condition?.Name))
            .ForMember(
                dest => dest.ImageUrls,
                opt => opt.MapFrom((src, dest) => src.Images.Select(im => im.Url)));
    }

    private static string? MapMonthName<TDestination>(Advert source, TDestination destination)
        => source.ManufacturedOn?.ToString("MMMM", new CultureInfo(GlobalConstants.BulgarianCultureInfo));

    private static int? MapYear<TDestination>(Advert source, TDestination destination)
        => source.ManufacturedOn?.Year;

    private static decimal? MapPrice<TDestination>(Advert source, TDestination destination)
        => source.PriceBgn;

    private static string? MapTransmission<TDestination>(Advert source, TDestination destination)
        => source.Transmission?.Name;

    private static string? MapBodyStyle<TDestination>(Advert source, TDestination destination)
        => source.BodyStyle?.Name;

    private static string? MapEngine<TDestination>(Advert source, TDestination destination)
        => source.Engine?.Name;
}
