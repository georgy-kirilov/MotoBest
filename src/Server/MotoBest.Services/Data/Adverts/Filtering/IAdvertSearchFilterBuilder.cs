using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts.Filtering;

public interface IAdvertSearchFilterBuilder
{
    IAdvertSearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts);
}
