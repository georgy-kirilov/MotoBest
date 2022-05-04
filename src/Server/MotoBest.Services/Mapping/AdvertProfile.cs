using AutoMapper;

using System.Globalization;

using MotoBest.Common;
using MotoBest.Data.Models;

using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Models;
using MotoBest.Services.Common.Units;

using MotoBest.WebApi.Models.Adverts.GetFullAdvert;
using MotoBest.WebApi.Models.Adverts.SearchAdverts;

namespace MotoBest.Services.Mapping;

public class AdvertProfile : Profile
{
    public AdvertProfile(IUnitManager unitManager)
    {
        CreateMap<GetFullAdvertResultModel, GetFullAdvertResponseModel>();

        CreateMap<SearchAdvertsResultModel, SearchAdvertsResponseModel>();

        CreateMap<SearchAdvertsRequestModel, SearchAdvertsInputModel>()
            .ForMember(m => m.MinMileageInKm, cfg => cfg.MapFrom(a => unitManager.ToKm(a.MileageUnit, a.MinMileage)))
            .ForMember(m => m.MaxMileageInKm, cfg => cfg.MapFrom(a => unitManager.ToKm(a.MileageUnit, a.MaxMileage)))
            .ForMember(m => m.MinPriceInBgn, cfg => cfg.MapFrom(a => unitManager.ToBgn(a.CurrencyUnit, a.MinPrice)))
            .ForMember(m => m.MaxPriceInBgn, cfg => cfg.MapFrom(a => unitManager.ToBgn(a.CurrencyUnit, a.MaxPrice)))
            .ForMember(m => m.MinPowerInHp, cfg => cfg.MapFrom(a => unitManager.ToHp(a.PowerUnit, a.MinPower)))
            .ForMember(m => m.MaxPowerInHp, cfg => cfg.MapFrom(a => unitManager.ToHp(a.PowerUnit, a.MaxPower)));

        CreateMap<Advert, SearchAdvertsResultModel>()
            .ForMember(m => m.PriceInBgn, cfg => cfg.MapFrom(a => a.PriceInBgn))
            .ForMember(m => m.Engine, cfg => cfg.MapFrom((x, y) => x.Engine?.Name))
            .ForMember(m => m.Year, cfg => cfg.MapFrom((x, y) => x.ManufacturedOn?.Year))
            .ForMember(m => m.Transmission, cfg => cfg.MapFrom((x, y) => x.Transmission?.Name))
            .ForMember(m => m.MainImageUrl, cfg => cfg.MapFrom(MapMainImageUrl))
            .ForMember(m => m.Month, cfg => cfg.MapFrom(MapMonth));

        CreateMap<Advert, GetFullAdvertResultModel>()
            .ForMember(m => m.PriceInBgn, cfg => cfg.MapFrom(a => a.PriceInBgn))
            .ForMember(m => m.Year, cfg => cfg.MapFrom((x, y) => x.ManufacturedOn?.Year))
            .ForMember(m => m.Month, cfg => cfg.MapFrom(MapMonth))
            .ForMember(m => m.Transmission, cfg => cfg.MapFrom((x, y) => x.Transmission?.Name))
            .ForMember(m => m.BodyStyle, cfg => cfg.MapFrom((x, y) => x.BodyStyle?.Name))
            .ForMember(m => m.Engine, cfg => cfg.MapFrom((x, y) => x.Engine?.Name))
            .ForMember(m => m.Brand, cfg => cfg.MapFrom((x, y) => x.Brand?.Name))
            .ForMember(m => m.Model, cfg => cfg.MapFrom((x, y) => x.Model?.Name))
            .ForMember(m => m.Color, cfg => cfg.MapFrom((x, y) => x.Color?.Name))
            .ForMember(m => m.Condition, cfg => cfg.MapFrom((x, y) => x.Condition?.Name))
            .ForMember(m => m.ImageUrls, cfg => cfg.MapFrom(MapImageUrls))
            .ForMember(m => m.OriginalAdvertUrl, cfg => cfg.MapFrom(MapOriginalAdvertUrl))
            .ForMember(m => m.Region, cfg => cfg.MapFrom((x, y) => x.Region?.Name))
            .ForMember(m => m.PopulatedPlace, cfg => cfg.MapFrom((x, y) => x.PopulatedPlace?.Name));
    }

    static IEnumerable<string> MapImageUrls<T>(Advert source, T destination)
        => source.Images.Where(im => im.Url != null).Select(im => im.Url)!;

    static string? MapMainImageUrl<T>(Advert source, T destination)
        => source.Images.LastOrDefault()?.Url;

    static string? MapMonth<T>(Advert source, T destination)
        => source.ManufacturedOn?.ToString("MMMM", new CultureInfo(GlobalConstants.BulgarianCultureInfo));

    static string? MapOriginalAdvertUrl<T>(Advert source, T destination)
    {
        if (source.Site == null)
        {
            return null;
        }

        string route = string.Format(
            source.Site.FullAdvertPagePathFormat,
            source.RemoteId,
            source.RemoteSlug);

        return $"https://{source.Site.Name}{route}";
    }
}
