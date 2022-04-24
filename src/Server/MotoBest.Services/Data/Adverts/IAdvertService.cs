using MotoBest.Services.Normalization;
using MotoBest.Services.Data.Adverts.Models;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddOrUpdate(NormalizedAdvert advert);

    DateTime? FindLatestAdvertModifiedOnDate(string site);

    IEnumerable<SearchAdvertResultServiceModel> SearchAdverts(
        SearchAdvertsServiceModel serviceModel,
        int pageIndex = AdvertServiceConstants.AdvertSearchResultsFirstPageIndex,
        int resultsPerPageCount = AdvertServiceConstants.AdvertSearchResultsPerPageCount);

    Task<FullAdvertServiceModel?> GetFullAdvert(string id);
}
