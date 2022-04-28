using MotoBest.Services.Normalization;
using MotoBest.Services.Data.Adverts.Models;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddOrUpdate(NormalizedAdvert advert);

    DateTime? FindLatestAdvertModifiedOnDate(string site);

    IEnumerable<SearchAdvertsResultModel> SearchAdverts(
        SearchAdvertsInputModel serviceModel,
        int pageIndex = AdvertServiceConstants.AdvertSearchResultsFirstPageIndex,
        int resultsPerPageCount = AdvertServiceConstants.AdvertSearchResultsPerPageCount);

    Task<GetFullAdvertResultModel?> GetFullAdvert(string id);
}
