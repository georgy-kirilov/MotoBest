using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface ISearchFilterOptionsBuilder
{
    IQueryable<Advert> ApplyFilter();

    ISearchFilterOptionsBuilder ByMaking(int? brandId, int? modelId);

    ISearchFilterOptionsBuilder ByBodyStyle(int? bodyStyleId);

    ISearchFilterOptionsBuilder ByColor(int? colorId);

    ISearchFilterOptionsBuilder ByCondition(int? conditionId);

    ISearchFilterOptionsBuilder ByEngine(int? engineId);

    ISearchFilterOptionsBuilder ByTransmission(int? transmissionId);

    ISearchFilterOptionsBuilder ByEuroStandard(int? euroStandardId);

    ISearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp);

    ISearchFilterOptionsBuilder ByMileage(int? minMileageInKm, int? maxMileageInKm);

    ISearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear);

    ISearchFilterOptionsBuilder ByLocation(int? regionId, int? populatedPlaceId);

    ISearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn);
}
