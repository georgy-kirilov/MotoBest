using AutoMapper;
using System.Globalization;

using MotoBest.Common;
using MotoBest.Data.Models;

using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;
using MotoBest.WebApi.Models.Adverts;
using MotoBest.Services.Common.Units;
using MotoBest.Common.Units;

namespace MotoBest.Services.Mapping;

public class AdvertProfile : Profile
{
    private readonly IUnitsManager unitsManager;

    public AdvertProfile(IUnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;

        CreateMap<SearchAdvertsRequestModel, SearchAdvertsServiceModel>()
            .ForMember(m => m.MinMileageInKm, cfg => cfg.MapFrom(a => unitsManager.ToKm(a.MileageUnit, a.MinMileage)))
            .ForMember(m => m.MaxMileageInKm, cfg => cfg.MapFrom(a => unitsManager.ToKm(a.MileageUnit, a.MaxMileage)))
            .ForMember(m => m.MinPriceInBgn, cfg => cfg.MapFrom(a => unitsManager.ToBgn(a.CurrencyUnit, a.MinPrice)))
            .ForMember(m => m.MaxPriceInBgn, cfg => cfg.MapFrom(a => unitsManager.ToBgn(a.CurrencyUnit, a.MaxPrice)))
            .ForMember(m => m.MinPowerInHp, cfg => cfg.MapFrom(a => unitsManager.ToHp(a.PowerUnit, a.MinPower)))
            .ForMember(m => m.MaxPowerInHp, cfg => cfg.MapFrom(a => unitsManager.ToHp(a.PowerUnit, a.MaxPower)));

        CreateMap<SearchAdvertResultServiceModel, SearchAdvertResultResponseModel>();

        CreateMap<Advert, SearchAdvertResultServiceModel>()
            .ForMember(dest => dest.PriceInBgn,opt => opt.MapFrom(MapPrice))
            .ForMember(
                dest => dest.Engine,
                opt => opt.MapFrom(MapEngine))
            .ForMember(
                dest => dest.Year,
                opt => opt.MapFrom(MapYear))
            .ForMember(
                dest => dest.Transmission,
                opt => opt.MapFrom(MapTransmission))
            .ForMember(
                dest => dest.MainImageUrl,
                opt => opt.MapFrom(MapMainImageUrl))
            .ForMember(
                dest => dest.Month,
                opt => opt.MapFrom(MapMonthName));

        CreateMap<Advert, FullAdvertServiceModel>()
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
                opt => opt.MapFrom(MapBrand))
            .ForMember(
                dest => dest.Model,
                opt => opt.MapFrom(MapModel))
            .ForMember(
                dest => dest.Color,
                opt => opt.MapFrom(MapColor))
            .ForMember(
                dest => dest.Condition,
                opt => opt.MapFrom(MapCondition))
            .ForMember(
                dest => dest.ImageUrls,
                opt => opt.MapFrom(MapImageUrls));
    }

    private string? MapBrand<T>(Advert source, T destination)
        => source.Brand?.Name;

    private string? MapModel<T>(Advert source, T destination)
        => source.Model?.Name;

    private string? MapCondition<T>(Advert source, T destination)
        => source.Condition?.Name;

    private string? MapColor<T>(Advert source, T destination)
        => source.Color?.Name;

    private IEnumerable<string> MapImageUrls<T>(Advert source, T destination)
        => source.Images.Where(im => im.Url != null).Select(im => im.Url)!;

    private string MapMainImageUrl<T>(Advert source, T destination)
        => source.Images.FirstOrDefault()?.Url ?? AdvertServiceConstants.DefaultAdvertImageUrl;

    private int? MapMileage<T>(SearchAdvertsRequestModel source, SearchAdvertsServiceModel destination)
        => ConvertMileageToKm(source.MileageUnit, source.MinMileage);

    private int? ConvertMileageToKm(MileageUnit mileageUnit, int? mileage)
        => (int) unitsManager.GetKmMultiplier(mileageUnit) * mileage;

    private string? MapMonthName<T>(Advert source, T destination)
        => source.ManufacturedOn?.ToString("MMMM", new CultureInfo(GlobalConstants.BulgarianCultureInfo));

    private int? MapYear<T>(Advert source, T destination)
        => source.ManufacturedOn?.Year;

    private decimal? MapPrice<T>(Advert source, T destination)
        => source.PriceInBgn;

    private string? MapTransmission<T>(Advert source, T destination)
        => source.Transmission?.Name;

    private string? MapBodyStyle<T>(Advert source, T destination)
        => source.BodyStyle?.Name;

    private string? MapEngine<T>(Advert source, T destination)
        => source.Engine?.Name;
}
