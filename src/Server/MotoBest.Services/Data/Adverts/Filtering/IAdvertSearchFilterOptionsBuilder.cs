using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface IAdvertSearchFilterOptionsBuilder
{
    IQueryable<Advert> ApplyFilter();

    IAdvertSearchFilterOptionsBuilder ByBodyStyle(string? bodyStyle);

    IAdvertSearchFilterOptionsBuilder ByBrand(string? brand);

    IAdvertSearchFilterOptionsBuilder ByColor(string? color);

    IAdvertSearchFilterOptionsBuilder ByCondition(string? condition);

    IAdvertSearchFilterOptionsBuilder ByEngine(string? engine);

    IAdvertSearchFilterOptionsBuilder ByRegion(string? region);

    IAdvertSearchFilterOptionsBuilder ByTransmission(string? transmission);

    IAdvertSearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp);

    IAdvertSearchFilterOptionsBuilder ByKilometrage(int? minMileageInKm, int? maxMileageInKm);

    IAdvertSearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear);

    IAdvertSearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn);
}
