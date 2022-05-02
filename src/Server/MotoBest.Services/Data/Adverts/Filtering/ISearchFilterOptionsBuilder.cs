using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface ISearchFilterOptionsBuilder
{
    IQueryable<Advert> ApplyFilter();

    ISearchFilterOptionsBuilder ByBodyStyle(string? bodyStyle);

    ISearchFilterOptionsBuilder ByColor(string? color);

    ISearchFilterOptionsBuilder ByCondition(string? condition);

    ISearchFilterOptionsBuilder ByEngine(string? engine);

    ISearchFilterOptionsBuilder ByTransmission(string? transmission);

    ISearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp);

    ISearchFilterOptionsBuilder ByKilometrage(int? minMileageInKm, int? maxMileageInKm);

    ISearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear);

    ISearchFilterOptionsBuilder ByMaking(string? brand, int? model);

    ISearchFilterOptionsBuilder ByLocation(string? region, int? populatedPlace);

    ISearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn);

    ISearchFilterOptionsBuilder ByEuroStandard(string? euroStandard);
}
