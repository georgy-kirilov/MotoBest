using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface ISearchFilterBuilder
{
    ISearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts);
}
