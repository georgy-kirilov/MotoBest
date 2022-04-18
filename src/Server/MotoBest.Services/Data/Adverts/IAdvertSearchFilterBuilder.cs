using MotoBest.Data.Models;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertSearchFilterBuilder
{
    IAdvertSearchFilterOptionsBuilder CreateFilterFor(IQueryable<Advert> adverts);
}
