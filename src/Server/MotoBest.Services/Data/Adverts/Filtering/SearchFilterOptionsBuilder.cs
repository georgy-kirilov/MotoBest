using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public class SearchFilterOptionsBuilder : ISearchFilterFactory, ISearchFilterOptionsBuilder
{
    private IQueryable<Advert> query = new List<Advert>().AsQueryable();

    public IQueryable<Advert> ApplyFilter() => query;

    public ISearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts)
    {
        query = adverts;
        return this;
    }

    public ISearchFilterOptionsBuilder ByMaking(int? brandId, int? modelId)
    {
        if (modelId != null)
        {
            query = query.Where(adv => adv.ModelId == modelId);
        }
        else
        {
            query = query.Where(adv => brandId == null || adv.BrandId == brandId);
        }

        return this;
    }

    public ISearchFilterOptionsBuilder ByBodyStyle(int? bodyStyleId)
    {
        query = query.Where(adv => bodyStyleId == null || adv.BodyStyleId == bodyStyleId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByEngine(int? engineId)
    {
        query = query.Where(adv => engineId == null || adv.EngineId == engineId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByTransmission(int? transmissionId)
    {
        query = query.Where(adv => transmissionId == null || adv.TransmissionId == transmissionId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByCondition(int? conditionId)
    {
        query = query.Where(adv => conditionId == null || adv.ConditionId == conditionId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByColor(int? colorId)
    {
        query = query.Where(adv => colorId == null || adv.ColorId == colorId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByEuroStandard(int? euroStandardId)
    {
        query = query.Where(adv => euroStandardId == null || adv.EuroStandardId == euroStandardId);
        return this;
    }

    public ISearchFilterOptionsBuilder ByLocation(int? regionId, int? populatedPlaceId)
    {
        if (populatedPlaceId != null)
        {
            query = query.Where(adv => adv.PopulatedPlaceId == populatedPlaceId);
        }
        else
        {
            query = query.Where(adv => regionId == null || adv.RegionId == regionId);
        }

        return this;
    }

    public ISearchFilterOptionsBuilder ByPower(int? minPowerInHp, int? maxPowerInHp)
    {
        query = query.Where(adv => minPowerInHp == null || adv.PowerInHp >= minPowerInHp);
        query = query.Where(adv => maxPowerInHp == null || adv.PowerInHp <= maxPowerInHp);
        return this;
    }

    public ISearchFilterOptionsBuilder ByMileage(int? minMileageInKm, int? maxMileageInKm)
    {
        query = query.Where(adv => minMileageInKm == null || adv.MileageInKm >= minMileageInKm);
        query = query.Where(adv => maxMileageInKm == null || adv.MileageInKm <= maxMileageInKm);
        return this;
    }

    public ISearchFilterOptionsBuilder ByYear(int? minYear, int? maxYear)
    {
        query = query.Where(adv => minYear == null || (adv.ManufacturedOn != null && adv.ManufacturedOn.Value.Year >= minYear));
        query = query.Where(adv => maxYear == null || (adv.ManufacturedOn != null && adv.ManufacturedOn.Value.Year <= maxYear));
        return this;
    }

    public ISearchFilterOptionsBuilder ByPrice(decimal? minPriceInBgn, decimal? maxPriceInBgn)
    {
        query = query.Where(adv => minPriceInBgn == null || adv.PriceInBgn >= minPriceInBgn);
        query = query.Where(adv => maxPriceInBgn == null || adv.PriceInBgn <= maxPriceInBgn);
        return this;
    }
}
