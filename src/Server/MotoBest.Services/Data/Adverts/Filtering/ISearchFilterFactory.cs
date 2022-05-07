using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface ISearchFilterFactory
{
    ISearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts);
}
