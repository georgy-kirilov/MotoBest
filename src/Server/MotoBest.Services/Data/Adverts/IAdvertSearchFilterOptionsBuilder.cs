using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts;

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

    IAdvertSearchFilterOptionsBuilder ByHorsePowers(int? minHorsePowers, int? maxHorsePowers);

    IAdvertSearchFilterOptionsBuilder ByKilometrage(int? minKilometrage, int? maxKilometrage);

    IAdvertSearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear);
}
